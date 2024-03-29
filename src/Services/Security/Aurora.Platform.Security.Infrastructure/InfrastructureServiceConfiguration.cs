﻿using Aurora.Platform.Security.Domain.Repositories;
using Aurora.Platform.Security.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aurora.Platform.Security.Infrastructure
{
    public static class InfrastructureServiceConfiguration
    {
        public static IServiceCollection AddSecurityInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Connection string
            services.AddDbContext<SecurityContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("SecurityDataConnection"),
                    x => x.MigrationsHistoryTable("__EFMigrationsHistory", "SEC")));

            // Repository implementations
            services.AddScoped<IApplicationRepository, ApplicationRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserSessionRepository, UserSessionRepository>();
            services.AddScoped<IUserTokenRepository, UserTokenRepository>();
            services.AddScoped<ICredentialLogRepository, CredentialLogRepository>();

            return services;
        }
    }
}