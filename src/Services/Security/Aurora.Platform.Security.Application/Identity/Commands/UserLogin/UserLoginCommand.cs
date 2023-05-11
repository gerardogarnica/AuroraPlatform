using Aurora.Framework.Security;
using Aurora.Platform.Security.Domain.Entities;
using Aurora.Platform.Security.Domain.Exceptions;
using Aurora.Platform.Security.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Aurora.Platform.Security.Application.Identity.Commands.UserLogin;

public record UserLoginCommand : IRequest<IdentityToken>
{
    public string LoginName { get; set; }
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
        var user = await GetUserAsync(request.LoginName, request.Password);
        var roles = await GetUserRolesAsync(user.UserRoles);

        // Get token information
        var userInfo = _mapper.Map<UserInfo>(user);
        userInfo.Roles = _mapper.Map<List<RoleInfo>>(roles);

        var tokenInfo = _securityHandler.GenerateTokenInfo(userInfo);

        // Updates token repository
        var entry = await UpdateUserTokenAsync(user.Token, tokenInfo);

        // Add user session
        await AddUserSessionAsync(user.Id, user.LoginName, entry);

        // Returns identity tokens
        return new IdentityToken()
        {
            AccessToken = tokenInfo.AccessToken,
            RefreshToken = tokenInfo.RefreshToken
        };
    }

    #endregion

    #region Private methods

    private async Task<User> GetUserAsync(string loginName, string password)
    {
        var user = await _userRepository.GetAsync(loginName) ?? throw new InvalidCredentialsException();

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

    private async Task<UserToken> UpdateUserTokenAsync(
        UserToken userToken, TokenInfo tokenInfo)
    {
        var entry = userToken;

        entry.AccessToken = tokenInfo.AccessToken;
        entry.AccessTokenExpiration = tokenInfo.AccessTokenExpiration;
        entry.RefreshToken = tokenInfo.RefreshToken;
        entry.RefreshTokenExpiration = tokenInfo.RefreshTokenExpiration;
        entry.IssuedDate = DateTime.UtcNow;

        return await _userTokenRepository.UpdateAsync(entry);
    }

    private async Task AddUserSessionAsync(int userId, string loginName, UserToken userToken)
    {
        var entry = new UserSession()
        {
            UserId = userId,
            LoginName = loginName,
            AccessToken = userToken.AccessToken,
            AccessTokenExpiration = userToken.AccessTokenExpiration.Value,
            RefreshToken = userToken.RefreshToken,
            RefreshTokenExpiration = userToken.RefreshTokenExpiration.Value,
            BeginSessionDate = DateTime.UtcNow
        };

        await _userSessionRepository.AddAsync(entry);
    }

    #endregion
}