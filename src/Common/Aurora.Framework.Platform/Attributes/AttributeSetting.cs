using Aurora.Framework.Settings;

namespace Aurora.Framework.Platform.Attributes
{
    public class AttributeSetting : AuroraAttributeSetting
    {
        public int AttributeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ScopeType { get; set; }
        public bool IsVisible { get; set; }
        public bool IsEditable { get; set; }
        public bool IsActive { get; set; }
    }
}