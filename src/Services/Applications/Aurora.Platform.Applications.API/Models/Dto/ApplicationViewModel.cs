namespace Aurora.Platform.Applications.API.Models
{
    public class ApplicationViewModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool HasCustomConfig { get; set; }
    }
}