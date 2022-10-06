using Aurora.Framework.Settings;

namespace Aurora.Platform.Settings.Application.Queries
{
    public class AttributeSettingModel : AuroraAttributeSetting
    {
        public int Id { get; set; }
        public string? Code{ get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ScopeType { get; set; }
        public bool IsVisible { get; set; }
        public bool IsEditable { get; set; }
        public bool IsActive { get; set; }
    }
}