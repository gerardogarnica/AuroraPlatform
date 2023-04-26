using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace Aurora.Framework.Security
{
    public static class AuthConfiguration
    {
        public static IServiceCollection AddAuthenticationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Add security token implementation
            services.AddScoped<ISecurityToken, SecurityToken>();

            // Get secret key value
            var secretKey = configuration.GetValue<string>("JWT:SecretKey");

            if (string.IsNullOrWhiteSpace(secretKey))
            {
                throw new Exceptions.PlatformException("SecretKey value not set.");
            }

            // Add auth configuration
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(o =>
                {
                    o.RequireHttpsMetadata = false;
                    o.SaveToken = true;
                    o.TokenValidationParameters = SecurityTokenProvider.GetValidationParameters(Encoding.ASCII.GetBytes(secretKey));
                });

            return services;
        }
    }
}