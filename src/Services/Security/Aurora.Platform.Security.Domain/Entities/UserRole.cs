using Aurora.Framework.Entities;

namespace Aurora.Platform.Security.Domain.Entities
{
    public class UserRole : AuditableEntity
    {
        public override int Id { get => base.Id; set => base.Id = value; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public bool IsDefault { get; set; }
        public bool IsActive { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }
    }
}