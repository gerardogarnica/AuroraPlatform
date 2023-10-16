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

        public UserRole() { }

        public UserRole(int userId, int roleId, bool isDefault, bool isActive)
        {
            UserId = userId;
            RoleId = roleId;
            IsDefault = isDefault;
            IsActive = isActive;
        }

        public void UpdateStatus(bool isAddAction)
        {
            if (IsDefault && !isAddAction) User.CheckIfIsUnableToChange();

            IsActive = isAddAction;
        }
    }
}