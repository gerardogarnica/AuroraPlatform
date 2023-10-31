using Aurora.Framework.Repositories;
using Aurora.Platform.Security.Domain.Entities;

namespace Aurora.Platform.Security.Infrastructure.Seeds
{
    public class ApplicationSeed : ISeedData<SecurityContext>
    {
        const string applicationCode = "DBB1F084-0E5C-488F-8990-EA1FDF223A94";

        public void Seed(SecurityContext context)
        {
            var platformApp = context
                .Applications
                .FirstOrDefault(x => x.Code.Equals(applicationCode));

            if (platformApp != null) return;

            context.Applications.Add(CreatePlatformApp());

            context.SaveChanges();
        }

        private static Application CreatePlatformApp()
        {
            return new Application()
            {
                Code = applicationCode,
                Name = "Aurora Platform",
                Description = "Management platform of Aurora Soft applications",
                HasCustomConfig = false
            };
        }
    }
}