using Aurora.Framework.Entities;

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
    }
}