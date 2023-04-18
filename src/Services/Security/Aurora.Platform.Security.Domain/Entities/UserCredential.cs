using Aurora.Framework.Entities;

namespace Aurora.Platform.Security.Domain.Entities
{
    public class UserCredential : AuditableEntity
    {
        public override int Id { get => base.Id; set => base.Id = value; }
        public string Password { get; set; }
        public string PasswordControl { get; set; }
        public bool MustChange { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public User User { get; set; }
    }
}