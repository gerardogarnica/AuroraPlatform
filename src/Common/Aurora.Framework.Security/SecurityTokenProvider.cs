using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Aurora.Framework.Security
{
    public static class SecurityTokenProvider
    {
        private static ClaimsPrincipal GetPrincipal(string token, string secretKey)
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