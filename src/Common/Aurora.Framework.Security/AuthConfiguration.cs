using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aurora.Framework.Security
{
    public static class AuthConfiguration
    {
        public static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add security token implementation
            services.AddScoped<IJwtSecurityHandler, JwtSecurityHandler>();

            // Get secret key value
            var secretKey = configuration.GetValue<string>("JWT:SecretKey");

            if (string.IsNullOrWhiteSpace(secretKey))
            {
                throw new PlatformException("SecretKey value not set.");
            }

            // Add auth configuration
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = true;
                    o.TokenValidationParameters = TokenParametersProvider.GetValidationParameters(secretKey);
                });

            return services;
        }
    }
}