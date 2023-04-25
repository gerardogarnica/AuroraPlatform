namespace Aurora.Framework.Security
{
    public class RoleInfo
    {
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsGlobal { get; set; }
        public bool IsActive { get; set; }
    }
}