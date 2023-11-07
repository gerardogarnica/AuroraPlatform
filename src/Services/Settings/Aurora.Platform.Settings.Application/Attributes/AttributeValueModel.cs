using Aurora.Framework;
using Aurora.Framework.Settings;

namespace Aurora.Platform.Settings.Application.Attributes
{
    public class AttributeValueModel : AuroraAttributeValue
    {
        public int AttributeId { get; set; }
        public int RelationshipId { get; set; }
        public string Code { get; set; }
        public string Notes { get; set; }
        public AuroraDataType DataType { get; set; }
        public AttributeSettingModel Setting { get; set; }
    }
}