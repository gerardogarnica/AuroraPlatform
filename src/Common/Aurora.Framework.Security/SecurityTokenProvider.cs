using Aurora.Framework.Exceptions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Aurora.Framework.Security
{
    public static class SecurityTokenProvider
    {
        public static TokenInfo GenerateTokenInfo(UserInfo user, JwtConfiguration configuration)
        {
            // Create claims from user
            var claims = CreateClaims(user);

            // Get the token descriptor
            var tokenDescriptor = CreateTokenDescriptor(claims, configuration.Key, configuration.TokenValidityInMinutes);

            // Create the security token
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.CreateToken(tokenDescriptor);

            // Return token information
            return new TokenInfo()
            {
                AccessToken = tokenHandler.WriteToken(securityToken),
                AccessTokenExpiration = securityToken.ValidTo,
                RefreshToken = GenerateRefreshToken(),
                RefreshTokenExpiration = DateTime.UtcNow.AddDays(configuration.RefreshTokenValidityInDays)
            };
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

        public static ClaimsPrincipal GetPrincipal(string token, string secretKey)
        {
            var tokenParameters = GetValidationParameters(Encoding.ASCII.GetBytes(secretKey));

            var tokenHandler = new JwtSecurityTokenHandler();
            var principal = tokenHandler.ValidateToken(token, tokenParameters, out var securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("There is an invalid security token.");

            return principal;
        }

        public static TokenValidationParameters GetValidationParameters(byte[] key)
        {
            return new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false
            };
        }
    }
}