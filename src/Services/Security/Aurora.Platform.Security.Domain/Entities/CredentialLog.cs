using Aurora.Framework.Entities;

namespace Aurora.Platform.Security.Domain.Entities
{
    public class CredentialLog : EntityBase
    {
        public override int Id { get => base.Id; set => base.Id = value; }
        public int UserId { get; set; }
        public string Password { get; set; }
        public string PasswordControl { get; set; }
        public int ChangeVersion { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? EndDate { get; set; }

        public CredentialLog() { }

        public CredentialLog(User user, int currentVersion)
        {
            UserId = user.Id;
            Password = user.Password;
            PasswordControl = user.PasswordControl;
            ChangeVersion = currentVersion + 1;
            ExpirationDate = user.PasswordExpirationDate;
            CreatedDate = DateTime.UtcNow;
        }
    }
}