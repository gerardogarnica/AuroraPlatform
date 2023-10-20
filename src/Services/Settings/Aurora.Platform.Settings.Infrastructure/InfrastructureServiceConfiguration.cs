using Aurora.Platform.Settings.Domain.Repositories;
using Aurora.Platform.Settings.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aurora.Platform.Settings.Infrastructure
{
    public static class InfrastructureServiceConfiguration
    {
        public static IServiceCollection AddSettingsInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Connection string
            services.AddDbContext<SettingsContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString("SettingsDataConnection"),
                    x => x.MigrationsHistoryTable("__EFMigrationsHistory", "SET")));

            // Repositories implementations
            services.AddScoped<IAttributeSettingRepository, AttributeSettingRepository>();
            services.AddScoped<IAttributeValueRepository, AttributeValueRepository>();
            services.AddScoped<IOptionsCatalogRepository, OptionsCatalogRepository>();

            return services;
        }
    }
}