using Aurora.Framework.Entities;

namespace Aurora.Platform.Settings.Domain.Entities
{
    public class AttributeValue : AuditableEntity
    {
        public int RelationshipId { get; set; }
        public string? Value { get; set; }
        public AttributeSetting AttributeSetting { get; set; } = new AttributeSetting();
    }
}