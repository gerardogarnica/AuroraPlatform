using Aurora.Framework.Exceptions;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Aurora.Framework.Security
{
    public interface ISecurityToken
    {
        TokenInfo GenerateTokenInfo(UserInfo user);
    }

    public class SecurityToken : ISecurityToken
    {
        private readonly IConfiguration _configuration;

        public SecurityToken(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        TokenInfo ISecurityToken.GenerateTokenInfo(UserInfo user)
        {
            try
            {
                var configuration = new JwtConfiguration(_configuration);

                // Create claims from user
                var claims = CreateClaims(user);

                // Get the token descriptor
                var tokenDescriptor = CreateTokenDescriptor(claims, configuration.Key, configuration.TokenValidityInMinutes);

                // Create the security token
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);

                // Returns token information
                return new TokenInfo()
                {
                    AccessToken = tokenHandler.WriteToken(securityToken),
                    AccessTokenExpiration = securityToken.ValidTo,
                    RefreshToken = GenerateRefreshToken(),
                    RefreshTokenExpiration = DateTime.UtcNow.AddDays(configuration.RefreshTokenValidityInDays)
                };
            }
            catch (Exception e)
            {
                var message = string.Format("There is an error while trying to generate a token. {0}", e.Message);
                throw new PlatformException(message, e);
            }
        }

        private static IList<Claim> CreateClaims(UserInfo user)
        {
            if (user == null)
            {
                throw new PlatformException("The user cannot be null.");
            }

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Sid, user.UserId.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.LoginName),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Description)
            };

            foreach (var role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }

            return claims;
        }

        private static SecurityTokenDescriptor CreateTokenDescriptor(IList<Claim> claims, string secretKey, int tokenValidityInMinutes)
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
    }
}