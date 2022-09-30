using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Aurora.Framework.Api
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwaggerGen(this IServiceCollection services, string apiName, string apiDescription, int versionNumber)
        {
            var version = string.Format("v{0}", versionNumber);

            services.AddSwaggerGen(a =>
            {
                a.SwaggerDoc(version, CreateOpenApiInfo(apiName, apiDescription, version));
            });

            return services;
        }

        public static IServiceCollection AddSwaggerGenWithApiKey(this IServiceCollection services, string apiName, string apiDescription, int versionNumber)
        {
            var version = string.Format("v{0}", versionNumber);
            var schemeName = "Bearer";

            services.AddSwaggerGen(a =>
            {
                a.SwaggerDoc(version, CreateOpenApiInfo(apiName, apiDescription, version));
                a.AddSecurityDefinition(schemeName, CreateOpenApiSecurityScheme(schemeName));
                a.AddSecurityRequirement(CreateOpenApiSecurityRequirement(schemeName));
            });

            return services;
        }

        private static OpenApiInfo CreateOpenApiInfo(string apiName, string apiDescription, string version)
        {
            return new OpenApiInfo()
            {
                Title = string.Format("{0} API", apiName),
                Description = apiDescription,
                Version = version,
                Contact = new OpenApiContact
                {
                    Name = "Aurora Support",
                    Email = "support@aurorasoft.ec"
                }
            };
        }

        private static OpenApiSecurityScheme CreateOpenApiSecurityScheme(string securitySchemeName)
        {
            return new OpenApiSecurityScheme()
            {
                BearerFormat = "JWT",
                Description = "JWT authorization header using Bearer scheme.\r\n\r\n" +
                    "Enter the word 'Bearer' [space] and the security token.\r\n\r\n" +
                    "Example: \"Bearer 12345abcdef\"",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = securitySchemeName
            };
        }

        private static OpenApiSecurityRequirement CreateOpenApiSecurityRequirement(string securitySchemeName)
        {
            return new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme()
                    {
                        Reference = new OpenApiReference()
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = securitySchemeName
                        },
                        Scheme = "oauth2",
                        Name = securitySchemeName,
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            };
        }
    }
}