using Microsoft.Extensions.Configuration;

namespace Aurora.Framework.Identity
{
    public class JwtConfiguration
    {
        public string Key { get; private set; }
        public int TokenValidityInMinutes { get; private set; }
        public int RefreshTokenValidityInDays { get; private set; }

        public JwtConfiguration(IConfiguration configuration)
        {
            Key = configuration.GetValue<string>("JWT:SecretKey");
            TokenValidityInMinutes = configuration.GetValue<int>("JWT:TokenValidityInMinutes");
            RefreshTokenValidityInDays = configuration.GetValue<int>("JWT:RefreshTokenValidityInDays");
        }
    }
}