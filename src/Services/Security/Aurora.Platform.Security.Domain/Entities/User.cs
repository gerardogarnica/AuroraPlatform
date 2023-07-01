using Aurora.Framework.Cryptography;
using Aurora.Framework.Entities;
using Aurora.Platform.Security.Domain.Exceptions;

namespace Aurora.Platform.Security.Domain.Entities
{
    public class User : AuditableEntity
    {
        private const string defaultPasswordControl = "Aurora.Platform.Security.UserData";

        public override int Id { get => base.Id; set => base.Id = value; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordControl { get; set; }
        public DateTime? PasswordExpirationDate { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public UserToken Token { get; set; }
        public List<UserRole> UserRoles { get; set; }

        public void EncryptPassword(string password, DateTime? expirationDate)
        {
            var passwordTuple = SymmetricEncryptionProvider
                .Protect(password, defaultPasswordControl);

            Password = passwordTuple.Item1;
            PasswordControl = passwordTuple.Item2;
            PasswordExpirationDate = expirationDate;
        }

        public void CheckIfPasswordMatches(string password)
        {
            var decryptPassword = SymmetricEncryptionProvider
                .Unprotect(Password, PasswordControl, defaultPasswordControl);

            if (password != decryptPassword)
                throw new InvalidCredentialsException();
        }

        public void CheckIfIsActive()
        {
            if (IsActive) return;

            throw new InactiveUserException(Email);
        }

        public void CheckIfIsUnableToChange()
        {
            if (!IsDefault) return;

            throw new UnableChangeUserException();
        }

        public void CheckIfPasswordHasExpired()
        {
            if (!PasswordExpirationDate.HasValue) return;

            if (DateTime.UtcNow > PasswordExpirationDate.Value.Date)
                throw new PasswordExpiredException();
        }
    }
}