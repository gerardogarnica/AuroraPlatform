using Aurora.Framework.Cryptography;
using Aurora.Framework.Entities;
using Aurora.Platform.Security.Domain.Exceptions;

namespace Aurora.Platform.Security.Domain.Entities
{
    public class User : AuditableEntity
    {
        public override int Id { get => base.Id; set => base.Id = value; }
        public string LoginName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public UserCredential Credential { get; set; }
        public List<UserRole> UserRoles { get; set; }

        public void CheckIfPasswordMatches(string password)
        {
            var decryptPassword = SymmetricEncryptionProvider
                .Unprotect(Credential.Password, Credential.PasswordControl, "Aurora.Platform.Security.UserData");

            if (password != decryptPassword)
                throw new InvalidCredentialsException();
        }

        public void CheckIfIsActive()
        {
            if (IsActive) return;

            throw new InactiveUserException(LoginName);
        }

        public void CheckIfPasswordHasExpired()
        {
            if (!Credential.MustChange) return;

            if (DateTime.UtcNow > Credential.ExpirationDate.Value.Date)
                throw new PasswordExpiredException();
        }
    }
}