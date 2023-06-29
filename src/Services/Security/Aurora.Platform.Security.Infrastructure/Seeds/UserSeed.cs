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
            context.CredentialLogs.Add(CreateAdminCredentialLog(adminUser));
        }

        private static User CreateAdminUser()
        {
            var passwordTuple = User.EncryptPassword("admin123");

            return new User()
            {
                FirstName = "Administrador",
                LastName = "Plataforma",
                Email = adminUserEmail,
                Password = passwordTuple.Item1,
                PasswordControl = passwordTuple.Item2,
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
        }

        private static CredentialLog CreateAdminCredentialLog(User user)
        {
            return new CredentialLog
            {
                UserId = user.Id,
                Password = user.Password,
                PasswordControl = user.PasswordControl,
                ChangeVersion = 1,
                CreatedDate = DateTime.UtcNow
            };
        }
    }
}