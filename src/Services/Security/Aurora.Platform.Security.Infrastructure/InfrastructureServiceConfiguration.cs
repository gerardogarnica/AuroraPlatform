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
                options => options.UseSqlServer(configuration.GetConnectionString("SecurityDataConnection")));

            // Repository implementations

            return services;
        }
    }
}