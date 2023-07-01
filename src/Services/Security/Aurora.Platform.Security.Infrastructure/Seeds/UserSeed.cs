using Aurora.Framework.Repositories;
using Aurora.Platform.Security.Domain.Entities;

namespace Aurora.Platform.Security.Infrastructure.Seeds
{
    public class UserSeed : ISeedData<SecurityContext>
    {
        const string adminUserEmail = "admin@aurorasoft.ec";
        const string userBatch = "BATCH-USR";

        public void Seed(SecurityContext context)
        {
            var adminUser = context
                .Users
                .FirstOrDefault(x => x.Email.Equals(adminUserEmail));

            if (adminUser != null) return;

            adminUser = CreateAdminUser();

            context.Users.Add(adminUser);
            context.CredentialLogs.Add(new CredentialLog(adminUser, 0));
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
    }
}