using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Aurora.Framework.Identity
{
    public static class TokenParametersProvider
    {
        public static TokenValidationParameters GetValidationParameters(string secretKey)
        {
            var key = Encoding.ASCII.GetBytes(secretKey);

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