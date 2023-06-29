using Aurora.Framework.Entities;

namespace Aurora.Platform.Security.Domain.Entities
{
    public class Role : AuditableEntity
    {
        public override int Id { get => base.Id; set => base.Id = value; }
        public string Application { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}