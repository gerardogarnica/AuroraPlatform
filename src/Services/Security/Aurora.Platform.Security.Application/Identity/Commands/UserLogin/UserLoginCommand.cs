using Aurora.Framework.Identity;
using Aurora.Platform.Security.Domain.Entities;
using Aurora.Platform.Security.Domain.Exceptions;
using Aurora.Platform.Security.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Aurora.Platform.Security.Application.Identity.Commands.UserLogin;

public record UserCredentials
{
    public string Email { get; init; }
    public string Password { get; init; }
}

public record UserLoginCommand : UserCredentials, IRequest<IdentityToken>
{
    public string Application { get; init; }
}

public class UserLoginHandler : IRequestHandler<UserLoginCommand, IdentityToken>
{
    #region Private members

    private readonly IMapper _mapper;
    private readonly IIdentityHandler _identityHandler;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserSessionRepository _userSessionRepository;
    private readonly IUserTokenRepository _userTokenRepository;

    #endregion

    #region Constructor

    public UserLoginHandler(
        IMapper mapper,
        IIdentityHandler identityHandler,
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IUserSessionRepository userSessionRepository,
        IUserTokenRepository userTokenRepository)
    {
        _mapper = mapper;
        _identityHandler = identityHandler;
        _userRepository = userRepository;
        _roleRepository = roleRepository;
        _userSessionRepository = userSessionRepository;
        _userTokenRepository = userTokenRepository;
    }

    #endregion

    #region IRequestHandler implementation

    async Task<IdentityToken> IRequestHandler<UserLoginCommand, IdentityToken>.Handle(
        UserLoginCommand request, CancellationToken cancellationToken)
    {
        // Get user
        var user = await GetUserAsync(request.Email, request.Password);

        // Get user roles
        var userInfo = _mapper.Map<UserInfo>(user);
        userInfo.Roles = await GetUserRolesAsync(user.Id, request.Application, user.UserRoles);

        // Get token information
        var tokenInfo = _identityHandler.GenerateTokenInfo(userInfo);

        // Updates token repository
        var userToken = user.Tokens.First(x => x.Application.Equals(request.Application));
        userToken.UpdateWithTokenInfo(tokenInfo);
        await _userTokenRepository.UpdateAsync(userToken);

        // Add user session
        var userSession = new UserSession(user.Id, request.Application, user.Email, tokenInfo);
        await _userSessionRepository.AddAsync(userSession);

        // Returns identity tokens
        return new IdentityToken()
        {
            AccessToken = tokenInfo.AccessToken,
            RefreshToken = tokenInfo.RefreshToken
        };
    }

    #endregion

    #region Private methods

    private async Task<User> GetUserAsync(string email, string password)
    {
        var user = await _userRepository.GetAsyncByEmail(email) ?? throw new InvalidCredentialsException();

        user.CheckIfPasswordMatches(password);
        user.CheckIfIsActive();
        user.CheckIfPasswordHasExpired();

        return user;
    }

    private async Task<List<RoleInfo>> GetUserRolesAsync(int userId, string application, IList<UserRole> userRoles)
    {
        var roles = await _roleRepository.GetListAsync(userId);
        roles = roles.Where(x => x.AppCode.Equals(application)).ToList();

        var rolesInfo = _mapper.Map<List<RoleInfo>>(roles);
        foreach (var roleInfo in rolesInfo)
        {
            if (!userRoles.Any(x => x.RoleId == roleInfo.RoleId)) continue;

            roleInfo.IsDefault = userRoles.First(x => x.RoleId == roleInfo.RoleId).IsDefault;
            roleInfo.IsActive = userRoles.First(x => x.RoleId == roleInfo.RoleId).IsActive;
        }

        return rolesInfo;
    }

    #endregion
}