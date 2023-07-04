using Aurora.Framework.Repositories;
using Aurora.Platform.Security.Domain.Entities;

namespace Aurora.Platform.Security.Infrastructure.Seeds
{
    public class UserSeed : ISeedData<SecurityContext>
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

            if (adminUser != null) return;

            var adminRole = context
                .Roles
                .FirstOrDefault(x => x.Application.Equals(applicationCode) && x.Name.Equals(adminRoleName));

            adminUser = CreateAdminUser();

            if (adminRole != null)
            {
                UserRole[] userRole = new[] { CreateAdminUserRole(adminRole.Id) };
                adminUser.UserRoles = userRole.ToList();
            }

            context.Users.Add(adminUser);
            context.SaveChanges();

            context.CredentialLogs.Add(new CredentialLog(adminUser, 0));
            context.SaveChanges();
        }

        private static User CreateAdminUser()
        {
            var user = new User()
            {
                FirstName = "Administrador",
                LastName = "Plataforma",
                Email = adminUserEmail,
                IsDefault = true,
                IsActive = true,
                CreatedBy = userBatch,
                CreatedDate = DateTime.UtcNow,
                LastUpdatedBy = userBatch,
                LastUpdatedDate = DateTime.UtcNow,
                Token = new UserToken()
                {
                    AccessToken = null,
                    RefreshToken = null,
                    IssuedDate = DateTime.UtcNow,
                }
            };

            user.EncryptPassword("admin123", null);

            return user;
        }

        private static UserRole CreateAdminUserRole(int roleId)
        {
            return new UserRole()
            {
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