using Aurora.Framework.Entities;

namespace Aurora.Platform.Settings.Domain.Entities
{
    public class AttributeSetting : EntityBase
    {
        public string? Code { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ScopeType { get; set; }
        public string? DataType { get; set; }
        public string? Configuration { get; set; }
        public bool IsVisible { get; set; }
        public bool IsEditable { get; set; }
        public bool IsActive { get; set; }
        public IList<AttributeValue>? Values { get; set; }
    }
}