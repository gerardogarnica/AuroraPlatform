using Aurora.Framework.Entities;
using Aurora.Platform.Security.Domain.Exceptions;

namespace Aurora.Platform.Security.Domain.Entities
{
    public class Role : AuditableEntity
    {
        public override int Id { get => base.Id; set => base.Id = value; }
        public string Name { get; set; }
        public string Application { get; set; }
        public string Description { get; set; }
        public Guid Guid { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }
        public List<UserRole> UserRoles { get; set; }

        public void CheckIfIsActive()
        {
            if (IsActive) return;

            throw new InactiveRoleException(Name);
        }
    }
}