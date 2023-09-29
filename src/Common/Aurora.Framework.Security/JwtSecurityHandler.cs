using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Aurora.Framework.Security
{
    public interface IJwtSecurityHandler
    {
        UserInfo UserInfo { get; }
        TokenInfo GenerateTokenInfo(UserInfo user);
        void ValidateToken(string token);
        string GetApplicationCode();
    }

    public class JwtSecurityHandler : IJwtSecurityHandler
    {
        #region Private members

        private readonly JwtConfiguration _jwtConfiguration;
        private readonly IHttpContextAccessor _contextAccessor;

        UserInfo IJwtSecurityHandler.UserInfo => GetUserInfoFromToken();

        #endregion

        #region Constructors

        public JwtSecurityHandler(
            IConfiguration configuration,
            IHttpContextAccessor contextAccessor)
        {
            _jwtConfiguration = new JwtConfiguration(configuration);
            _contextAccessor = contextAccessor;
        }

        #endregion

        #region IJwtSecurityHandler implementation

        TokenInfo IJwtSecurityHandler.GenerateTokenInfo(UserInfo user)
        {
            try
            {
                // Create claims from user
                var claims = CreateClaims(user);

                // Get the token descriptor
                var tokenDescriptor = CreateTokenDescriptor(claims, _jwtConfiguration.Key, _jwtConfiguration.TokenValidityInMinutes);

                // Create the security token
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);

                // Returns token information
                return new TokenInfo()
                {
                    AccessToken = tokenHandler.WriteToken(securityToken),
                    AccessTokenExpiration = securityToken.ValidTo,
                    RefreshToken = GenerateRefreshToken(),
                    RefreshTokenExpiration = DateTime.UtcNow.AddDays(_jwtConfiguration.RefreshTokenValidityInDays)
                };
            }
            catch (Exception e)
            {
                var message = string.Format("There is an error while trying to generate a token. {0}", e.Message);
                throw new PlatformException(message, e);
            }
        }

        void IJwtSecurityHandler.ValidateToken(string token)
        {
            if (token == null) throw new ApiAuthorizationException();

            var tokenHandler = new JwtSecurityTokenHandler();
            tokenHandler.ValidateToken(
                token,
                TokenParametersProvider.GetValidationParameters(_jwtConfiguration.Key),
                out var securityToken);

            if (securityToken is not JwtSecurityToken)
                throw new SecurityTokenException("There is an invalid security token.");

            if (securityToken.ValidTo <= DateTime.UtcNow)
                throw new ApiAuthorizationException();
        }

        string IJwtSecurityHandler.GetApplicationCode()
        {
            var user = GetUserInfoFromToken();

            if (user == null) return null;
            if (!user.Roles.Any()) return null;

            return user.Roles.First().AppCode;
        }

        #endregion

        #region Private methods

        private static IList<Claim> CreateClaims(UserInfo user)
        {
            if (user == null)
            {
                throw new PlatformException("The user cannot be null.");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Sid, user.UserId.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Description),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.SerialNumber, user.Guid.ToString()),
                new Claim(ClaimTypes.UserData, user.InternalUserData)
            };

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.InternalRoleData));
            }

            return claims;
        }

        private static SecurityTokenDescriptor CreateTokenDescriptor(
            IList<Claim> claims, string secretKey, int tokenValidityInMinutes)
        {
            var key = Encoding.ASCII.GetBytes(secretKey);

            return new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(tokenValidityInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        private UserInfo GetUserInfoFromToken()
        {
            var principal = _contextAccessor.HttpContext.User;

            if (principal == null) return null;

            return new UserInfo(principal.Claims.ToList());
        }

        #endregion
    }
}