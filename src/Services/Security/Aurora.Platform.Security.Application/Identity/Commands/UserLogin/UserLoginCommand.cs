using Aurora.Framework.Security;
using Aurora.Platform.Security.Domain.Entities;
using Aurora.Platform.Security.Domain.Exceptions;
using Aurora.Platform.Security.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Aurora.Platform.Security.Application.Identity.Commands.UserLogin;

public record UserLoginCommand : IRequest<IdentityToken>
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public class UserLoginHandler : IRequestHandler<UserLoginCommand, IdentityToken>
{
    #region Private members

    private readonly IMapper _mapper;
    private readonly IJwtSecurityHandler _securityHandler;
    private readonly IRoleRepository _roleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserSessionRepository _userSessionRepository;
    private readonly IUserTokenRepository _userTokenRepository;

    #endregion

    #region Constructor

    public UserLoginHandler(
        IMapper mapper,
        IJwtSecurityHandler securityHandler,
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IUserSessionRepository userSessionRepository,
        IUserTokenRepository userTokenRepository)
    {
        _mapper = mapper;
        _securityHandler = securityHandler;
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
        // Get user and roles
        var user = await GetUserAsync(request.Email, request.Password);
        var roles = await GetUserRolesAsync(user.UserRoles);

        // Get token information
        var userInfo = _mapper.Map<UserInfo>(user);
        userInfo.Roles = _mapper.Map<List<RoleInfo>>(roles);

        var tokenInfo = _securityHandler.GenerateTokenInfo(userInfo);

        // Updates token repository
        user.Token.UpdateWithTokenInfo(tokenInfo);
        await _userTokenRepository.UpdateAsync(user.Token);

        // Add user session
        var userSession = new UserSession(user.Id, user.Email, tokenInfo);
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
        var user = await _userRepository.GetAsync(email) ?? throw new InvalidCredentialsException();

        user.CheckIfPasswordMatches(password);
        user.CheckIfIsActive();
        user.CheckIfPasswordHasExpired();

        return user;
    }

    private async Task<IList<Role>> GetUserRolesAsync(IList<UserRole> userRoles)
    {
        var roles = new List<Role>();

        foreach (var userRole in userRoles)
        {
            roles.Add(await _roleRepository.GetAsync(x => x.Id == userRole.RoleId));
        }

        return roles;
    }

    #endregion
}