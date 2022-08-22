using Aurora.Platform.Settings.Domain.Repositories;
using Aurora.Platform.Settings.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aurora.Platform.Settings.Infrastructure
{
    public static class ServiceConfiguration
    {
        public static IServiceCollection AddSettingsRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            // Repositories implementations
            services.AddScoped<IOptionsListRepository, OptionsListRepository>();

            return services;
        }
    }
}