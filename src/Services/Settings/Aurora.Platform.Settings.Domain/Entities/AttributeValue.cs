using Aurora.Framework.Entities;

namespace Aurora.Platform.Settings.Domain.Entities
{
    public class AttributeValue : AuditableEntity
    {
        public override int Id { get => base.Id; set => base.Id = value; }
        public int RelationshipId { get; set; }
        public string Value { get; set; }
        public string Notes { get; set; }
        public AttributeSetting AttributeSetting { get; set; }
    }
}