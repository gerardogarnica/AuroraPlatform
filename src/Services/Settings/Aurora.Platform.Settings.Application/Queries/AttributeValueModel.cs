using Aurora.Framework;
using Aurora.Framework.Settings;

namespace Aurora.Platform.Settings.Application.Queries
{
    public class AttributeValueModel : AuroraAttributeValue
    {
        public int Id { get; set; }
        public int RelationshipId { get; set; }
        public string Code { get; set; }
        public AuroraDataType DataType { get; set; }
    }
}