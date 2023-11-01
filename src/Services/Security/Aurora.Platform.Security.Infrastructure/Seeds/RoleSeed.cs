using Aurora.Framework.Repositories;
using Aurora.Platform.Security.Domain.Entities;

namespace Aurora.Platform.Security.Infrastructure.Seeds
{
    public class RoleSeed : ISeedData<SecurityContext>
    {
        const string adminRoleName = "Administradores";
        const string applicationCode = "DBB1F084-0E5C-488F-8990-EA1FDF223A94";
        const string userBatch = "BATCH-USR";

        public void Seed(SecurityContext context)
        {
            var adminRole = context
                .Roles
                .FirstOrDefault(x => x.Application.Equals(applicationCode) && x.Name.Equals(adminRoleName));

            if (adminRole != null) return;

            context.Roles.Add(CreateAdminRole());

            context.SaveChanges();
        }

        private static Role CreateAdminRole()
        {
            return new Role()
            {
                Name = adminRoleName,
                Application = applicationCode,
                Description = "Administradores de la Plataforma",
                IsActive = true,
                CreatedBy = userBatch,
                CreatedDate = DateTime.UtcNow,
                LastUpdatedBy = userBatch,
                LastUpdatedDate = DateTime.UtcNow
            };
        }
    }
}