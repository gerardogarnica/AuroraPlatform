using Aurora.Framework.Security;
using Aurora.Platform.Security.Domain.Entities;
using Aurora.Platform.Security.Domain.Exceptions;
using Aurora.Platform.Security.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Aurora.Platform.Security.Application.Identity.Commands.UserLogin;

public record UserCredentials
{
    public string Email { get; set; }
    public string Password { get; set; }
}

public record UserLoginCommand : UserCredentials, IRequest<IdentityToken>
{
    public string Application { get; set; }
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
        // Get user
        var user = await GetUserAsync(request.Email, request.Password);

        // Get user roles
        var userInfo = _mapper.Map<UserInfo>(user);
        userInfo.Roles = await GetUserRolesAsync(user.UserRoles, request.Application);

        // Get token information
        var tokenInfo = _securityHandler.GenerateTokenInfo(userInfo);

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
        var user = await _userRepository.GetAsync(email) ?? throw new InvalidCredentialsException();

        user.CheckIfPasswordMatches(password);
        user.CheckIfIsActive();
        user.CheckIfPasswordHasExpired();

        return user;
    }

    private async Task<List<RoleInfo>> GetUserRolesAsync(IList<UserRole> userRoles, string application)
    {
        var roles = new List<Role>();

        foreach (var userRole in userRoles)
        {
            if (!userRole.IsActive) continue;

            var role = await _roleRepository.GetAsync(x => x.Id == userRole.RoleId);

            if (role.Application.Equals(application)) roles.Add(role);
        }

        return _mapper.Map<List<RoleInfo>>(roles);
    }

    #endregion
}