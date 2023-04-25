namespace Aurora.Framework.Security
{
    public class UserInfo
    {
        public int UserId { get; set; }
        public string LoginName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description
        {
            get
            {
                return string.Format("{0} {1}", FirstName, LastName).Trim();
            }
        }
        public string Email { get; set; }
        public bool PasswordMustChange { get; set; }
        public DateTime? PasswordExpirationDate { get; set; }
        public bool IsDefaultUser { get; set; }
        public bool IsActive { get; set; }
        public IList<RoleInfo> Roles { get; set; }
    }
}