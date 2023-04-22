using Aurora.Framework.Entities;

namespace Aurora.Platform.Security.Domain.Entities
{
    public class UserCredentialLog : EntityBase
    {
        public override int Id { get => base.Id; set => base.Id = value; }
        public int UserId { get; set; }
        public int ChangeVersion { get; set; }
        public string Password { get; set; }
        public string PasswordControl { get; set; }
        public bool MustChange { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? EndDate { get; set; }
        public UserCredential Credential { get; set; }
    }
}