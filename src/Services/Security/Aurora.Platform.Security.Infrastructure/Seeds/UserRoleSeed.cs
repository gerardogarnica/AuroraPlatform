using Aurora.Framework.Repositories;
using Aurora.Platform.Security.Domain.Entities;

namespace Aurora.Platform.Security.Infrastructure.Seeds
{
    public class UserRoleSeed : ISeedData<SecurityContext>
    {
        const string adminUserEmail = "admin@aurorasoft.ec";
        const string adminRoleName = "Administradores";
        const string applicationCode = "DBB1F084-0E5C-488F-8990-EA1FDF223A94";
        const string userBatch = "BATCH-USR";

        public void Seed(SecurityContext context)
        {
            var adminUser = context
                .Users
                .FirstOrDefault(x => x.Email.Equals(adminUserEmail));

            if (adminUser == null) return;

            var adminRole = context
                .Roles
                .FirstOrDefault(x => x.Application.Equals(applicationCode) && x.Name.Equals(adminRoleName));

            if (adminRole == null) return;

            var userRole = context
                .UserRoles
                .FirstOrDefault(x => x.UserId.Equals(adminUser.Id) && x.RoleId.Equals(adminRole.Id));

            if (userRole != null) return;

            context.UserRoles.Add(CreateAdminUserRole(adminUser.Id, adminRole.Id));
        }

        private static UserRole CreateAdminUserRole(int userId, int roleId)
        {
            return new UserRole()
            {
                UserId = userId,
                RoleId = roleId,
                IsDefault = true,
                IsActive = true,
                CreatedBy = userBatch,
                CreatedDate = DateTime.UtcNow,
                LastUpdatedBy = userBatch,
                LastUpdatedDate = DateTime.UtcNow
            };
        }
    }
}