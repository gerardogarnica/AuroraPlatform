using Aurora.Framework.Repositories;
using Aurora.Platform.Security.Domain.Entities;

namespace Aurora.Platform.Security.Infrastructure.Seeds
{
    public class RoleSeed : ISeedData<SecurityContext>
    {
        public void Seed(SecurityContext context)
        {
            var adminRole = context
                .Roles
                .FirstOrDefault(x => x.IsGlobal && x.Name.Equals("Administradores"));

            if (adminRole != null) return;

            context.Roles.Add(CreateAdminRole());
        }

        private Role CreateAdminRole()
        {
            return new Role()
            {
                Name = "Administradores",
                Description = "Administradores de la Plataforma",
                IsGlobal = true,
                IsActive = true,
                CreatedBy = "BATCH-USR",
                CreatedDate = DateTime.UtcNow,
                LastUpdatedBy = "BATCH-USR",
                LastUpdatedDate = DateTime.UtcNow
            };
        }
    }
}