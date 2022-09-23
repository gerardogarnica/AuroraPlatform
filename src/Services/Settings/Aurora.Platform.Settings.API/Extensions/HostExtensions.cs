using Aurora.Platform.Settings.Infrastructure;
using Aurora.Platform.Settings.Infrastructure.Seed;
using Microsoft.EntityFrameworkCore;

namespace Aurora.Platform.Settings.API.Extensions
{
    public static class HostExtensions
    {
        public static IHost MigrateDatabase(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using var context = scope.ServiceProvider.GetRequiredService<SettingsContext>();

                context.Database.Migrate();
                SettingsContextSeed.SeedAsync(context).Wait();
            }

            return host;
        }
    }
}