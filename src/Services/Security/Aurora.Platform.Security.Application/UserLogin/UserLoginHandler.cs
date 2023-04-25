using Aurora.Framework.Security;
using Aurora.Platform.Security.Domain.Entities;
using Aurora.Platform.Security.Domain.Exceptions;
using Aurora.Platform.Security.Domain.Repositories;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace Aurora.Platform.Security.Application.UserLogin
{
    public class UserLoginHandler : IRequestHandler<UserLoginCommand, IdentityToken>
    {
        #region Private members

        private readonly IMapper _mapper;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserSessionRepository _userSessionRepository;
        private readonly IUserTokenRepository _userTokenRepository;
        private readonly JwtConfiguration _configuration;

        #endregion

        #region Constructor

        public UserLoginHandler(
            IConfiguration configuration,
            IMapper mapper,
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IUserSessionRepository userSessionRepository,
            IUserTokenRepository userTokenRepository)
        {
            _mapper = mapper;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userSessionRepository = userSessionRepository;
            _userTokenRepository = userTokenRepository;
            _configuration = new JwtConfiguration(configuration);
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

            var tokenInfo = SecurityTokenProvider.GenerateTokenInfo(userInfo, _configuration);

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
                AccessTokenExpiration = userToken.AccessTokenExpiration,
                RefreshToken = userToken.RefreshToken,
                RefreshTokenExpiration = userToken.RefreshTokenExpiration,
                BeginSessionDate = DateTime.UtcNow
            };

            await _userSessionRepository.AddAsync(entry);
        }

        #endregion
    }
}