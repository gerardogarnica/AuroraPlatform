using Aurora.Platform.Security.Domain;
using Aurora.Platform.Security.Domain.Entities;
using Aurora.Platform.Security.Domain.Exceptions;
using Aurora.Platform.Security.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace Aurora.Platform.Security.Application.UserLogin
{
    public class UserLoginHandler : IRequestHandler<UserLoginCommand, IdentityAccess>
    {
        #region Private members

        private readonly IConfiguration _configuration;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserSessionRepository _userSessionRepository;
        private readonly IUserTokenRepository _userTokenRepository;

        #endregion

        #region Constructor

        public UserLoginHandler(
            IConfiguration configuration,
            IUserRepository userRepository,
            IRoleRepository roleRepository,
            IUserSessionRepository userSessionRepository,
            IUserTokenRepository userTokenRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userSessionRepository = userSessionRepository;
            _userTokenRepository = userTokenRepository;
        }

        #endregion

        #region IRequestHandler implementation

        async Task<IdentityAccess> IRequestHandler<UserLoginCommand, IdentityAccess>.Handle(
            UserLoginCommand request, CancellationToken cancellationToken)
        {
            // Get user and roles
            var user = await GetUserAsync(request.LoginName, request.Password);
            var roles = await GetUserRolesAsync(user.UserRoles);

            // Create claims from user
            var claims = SecurityTokenProvider.CreateClaims(user, roles);

            // Get the token descriptor
            var secretKey = _configuration.GetValue<string>("JWT:SecretKey");
            var tokenValidityInMinutes = _configuration.GetValue<int>("JWT:TokenValidityInMinutes");
            var tokenDescriptor = SecurityTokenProvider.CreateTokenDescriptor(claims, secretKey, tokenValidityInMinutes);

            // Create the security token
            var tokenHandler = new JwtSecurityTokenHandler();
            var accessToken = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = SecurityTokenProvider.GenerateRefreshToken();

            // Updates token repository
            var entry = await UpdateUserTokenAsync(user.Token, tokenHandler, accessToken, refreshToken);

            // Add user session
            await AddUserSessionAsync(user.Id, user.LoginName, entry);

            // Returns token information
            return new IdentityAccess()
            {
                AccessToken = entry.AccessToken,
                RefreshToken = entry.RefreshToken
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
            UserToken userToken, JwtSecurityTokenHandler tokenHandler, SecurityToken accessToken, string refreshToken)
        {
            var entry = userToken;

            entry.AccessToken = tokenHandler.WriteToken(accessToken);
            entry.AccessTokenExpirationDate = accessToken.ValidTo;
            entry.RefreshToken = refreshToken;
            entry.RefreshTokenExpirationDate = DateTime.UtcNow.AddDays(_configuration.GetValue<int>("JWT:RefreshTokenValidityInDays"));
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
                AccessTokenExpirationDate = userToken.AccessTokenExpirationDate,
                RefreshToken = userToken.RefreshToken,
                RefreshTokenExpirationDate = userToken.RefreshTokenExpirationDate,
                BeginSessionDate = DateTime.UtcNow
            };

            await _userSessionRepository.AddAsync(entry);
        }

        #endregion
    }
}