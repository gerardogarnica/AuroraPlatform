using Aurora.Framework.Entities;

namespace Aurora.Platform.Settings.Domain.Entities
{
    public class AttributeSetting : EntityBase
    {
        public override int Id { get => base.Id; set => base.Id = value; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ScopeType { get; set; }
        public string DataType { get; set; }
        public string Configuration { get; set; }
        public bool IsVisible { get; set; }
        public bool IsEditable { get; set; }
        public bool IsActive { get; set; }
        public List<AttributeValue> Values { get; set; }
    }
}