using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Aurora.Framework.Repositories
{
    public static class DataMigrateExtensions
    {
        public static void MigrateData<T>(this IServiceProvider provider, IConfiguration configuration) where T : DbContext
        {
            try
            {
                var migrateData = configuration.GetValue<bool>("DataMigrate:Migrate");
                var seedData = configuration.GetValue<bool>("DataMigrate:Seed");

                using var scope = provider.CreateScope();
                var context = scope.ServiceProvider.GetRequiredService<T>();

                if (migrateData) context.Database.Migrate();

                if (seedData)
                {
                    typeof(T).Assembly.GetTypes()
                        .Where(t => t.IsClass && t.GetInterfaces().Contains(typeof(ISeedData<T>)))
                        .ToList()
                        .ForEach(t =>
                        {
                            var instance = Activator.CreateInstance(t);
                            var seedMethod = t?.GetMethod("Seed");
                            seedMethod?.Invoke(instance, new object[] { context });
                        });

                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                var message = string.Format("There is an error while trying to migrate data. {0}", e.Message);
                throw new PlatformException(message, e);
            }
        }
    }
}