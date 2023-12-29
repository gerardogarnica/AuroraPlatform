using Aurora.Framework.Settings;

namespace Aurora.Framework.Platform.Attributes
{
    public class AttributeValue : AuroraAttributeValue
    {
        public int AttributeId { get; set; }
        public int RelationshipId { get; set; }
        public string Code { get; set; }
        public string Notes { get; set; }
        public AuroraDataType DataType { get; set; }
        public AttributeSetting Setting { get; set; }
    }
}