using Aurora.Framework.Cryptography;
using Aurora.Framework.Repositories;
using Aurora.Platform.Security.Domain.Entities;

namespace Aurora.Platform.Security.Infrastructure.Seeds
{
    public class UserSeed : ISeedData<SecurityContext>
    {
        public void Seed(SecurityContext context)
        {
            var adminUser = context
                .Users
                .FirstOrDefault(x => x.LoginName.Equals("admin"));

            if (adminUser != null) return;

            context.Users.Add(CreateAdminUser());
        }

        private User CreateAdminUser()
        {
            return new User()
            {
                LoginName = "admin",
                FirstName = "Administrador",
                LastName = "Plataforma",
                Email = "admin@aurorasoft.ec",
                IsDefault = true,
                IsActive = true,
                CreatedBy = "BATCH-USR",
                CreatedDate = DateTime.UtcNow,
                LastUpdatedBy = "BATCH-USR",
                LastUpdatedDate = DateTime.UtcNow,
                Credential = CreateAdminCredential(),
                Token = CreateAdminToken()
            };
        }

        private UserCredential CreateAdminCredential()
        {
            var passwordTuple = SymmetricEncryptionProvider.Protect("admin", "Aurora.Platform.Security.UserData");

            return new UserCredential()
            {
                Password = passwordTuple.Item1,
                PasswordControl = passwordTuple.Item2,
                MustChange = false,
                CreatedBy = "BATCH-USR",
                CreatedDate = DateTime.UtcNow,
                LastUpdatedBy = "BATCH-USR",
                LastUpdatedDate = DateTime.UtcNow,
                CredentialLogs = CreateAdminCredentialLogs(passwordTuple.Item1, passwordTuple.Item2)
            };
        }

        private List<UserCredentialLog> CreateAdminCredentialLogs(string password, string control)
        {
            return new List<UserCredentialLog>
            {
                new UserCredentialLog()
                {
                    ChangeVersion = 1,
                    Password = password,
                    PasswordControl = control,
                    MustChange = false,
                    CreatedDate = DateTime.UtcNow
                }
            };
        }

        private UserToken CreateAdminToken()
        {
            return new UserToken()
            {
                AccessToken = null,
                RefreshToken = null,
                IssuedDate = DateTime.UtcNow,
            };
        }
    }
}