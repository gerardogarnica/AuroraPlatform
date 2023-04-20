using Aurora.Platform.Security.Domain;
using Aurora.Platform.Security.Domain.Entities;
using Aurora.Platform.Security.Domain.Exceptions;
using Aurora.Platform.Security.Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace Aurora.Platform.Security.Application.UserLogin
{
    public class UserLoginHandler : IRequestHandler<UserLoginCommand, IdentityAccess>
    {
        #region Private members

        private readonly IConfiguration _configuration;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;

        #endregion

        #region Constructor

        public UserLoginHandler(
            IConfiguration configuration,
            IUserRepository userRepository,
            IRoleRepository roleRepository)
        {
            _configuration = configuration;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
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
            var secretKey = _configuration.GetValue<string>("SecretKey");
            var tokenValidityInMinutes = _configuration.GetValue<int>("TokenValidityInMinutes");
            var tokenDescriptor = SecurityTokenProvider.CreateTokenDescriptor(claims, secretKey, tokenValidityInMinutes);

            // Create the security token
            var tokenHandler = new JwtSecurityTokenHandler();
            var accessToken = tokenHandler.CreateToken(tokenDescriptor);
            var refreshToken = SecurityTokenProvider.GenerateRefreshToken();

            return new IdentityAccess()
            {
                AccessToken = tokenHandler.WriteToken(accessToken),
                RefreshToken = refreshToken
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

        #endregion
    }
}