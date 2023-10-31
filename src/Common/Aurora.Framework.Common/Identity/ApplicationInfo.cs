namespace Aurora.Framework.Identity
{
    public class ApplicationInfo
    {
        public int ApplicationId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool HasCustomConfig { get; set; }
    }
}