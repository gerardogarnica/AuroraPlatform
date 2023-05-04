using Aurora.Framework.Repositories;
using Aurora.Platform.Security.Domain.Entities;

namespace Aurora.Platform.Security.Infrastructure.Seeds
{
    public class UserRoleSeed : ISeedData<SecurityContext>
    {
        public void Seed(SecurityContext context)
        {
            var adminUser = context
                .Users
                .FirstOrDefault(x => x.LoginName.Equals("admin"));

            if (adminUser == null) return;

            var adminRole = context
                .Roles
                .FirstOrDefault(x => x.IsGlobal && x.Name.Equals("Administradores"));

            if (adminRole == null) return;

            var userRole = context
                .UserRoles
                .FirstOrDefault(x => x.UserId.Equals(adminUser.Id) && x.RoleId.Equals(adminRole.Id));

            if (userRole != null) return;

            context.UserRoles.Add(CreateAdminUserRole(adminUser.Id, adminRole.Id));
        }

        private UserRole CreateAdminUserRole(int userId, int roleId)
        {
            return new UserRole()
            {
                UserId = userId,
                RoleId = roleId,
                IsDefault = true,
                IsActive = true,
                CreatedBy = "BATCH-USR",
                CreatedDate = DateTime.UtcNow,
                LastUpdatedBy = "BATCH-USR",
                LastUpdatedDate = DateTime.UtcNow
            };
        }
    }
}