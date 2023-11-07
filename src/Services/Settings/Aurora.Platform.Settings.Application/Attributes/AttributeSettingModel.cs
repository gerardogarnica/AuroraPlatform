using Aurora.Framework;
using Aurora.Framework.Settings;
using Aurora.Platform.Settings.Domain.Entities;

namespace Aurora.Platform.Settings.Application.Attributes
{
    public class AttributeSettingModel : AuroraAttributeSetting
    {
        public int AttributeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ScopeType { get; set; }
        public bool IsVisible { get; set; }
        public bool IsEditable { get; set; }
        public bool IsActive { get; set; }

        public AttributeValue GetDefaultAttributeValue(AttributeSetting setting, int relationshipId)
        {
            return new AttributeValue()
            {
                Id = AttributeId,
                RelationshipId = relationshipId,
                Value = GetDefaultValue(),
                AttributeSetting = setting
            };
        }

        private string GetDefaultValue()
        {
            switch (DataType)
            {
                case AuroraDataType.Boolean:
                    var booleanValue = new BooleanAttributeValue()
                    {
                        Value = BooleanSetting.DefaultValue
                    };
                    return booleanValue.GetValueWrapper();

                case AuroraDataType.Integer:
                    var integerValue = new IntegerAttributeValue()
                    {
                        Value = IntegerSetting.DefaultValue
                    };
                    return integerValue.GetValueWrapper(IntegerSetting);

                case AuroraDataType.Money:
                    var moneyValue = new MoneyAttributeValue()
                    {
                        Value = MoneySetting.DefaultValue
                    };
                    return moneyValue.GetValueWrapper(MoneySetting);

                case AuroraDataType.Numeric:
                    var numericValue = new NumericAttributeValue()
                    {
                        Value = NumericSetting.DefaultValue
                    };
                    return numericValue.GetValueWrapper(NumericSetting);

                case AuroraDataType.Options:
                    var optionListValue = new OptionsAttributeValue()
                    {
                        ItemCodes = OptionsSetting.DefaultItemCodes
                    };
                    return optionListValue.GetValueWrapper(OptionsSetting);

                case AuroraDataType.Text:
                    var textValue = new TextAttributeValue()
                    {
                        Value = TextSetting.DefaultValue
                    };
                    return textValue.GetValueWrapper(TextSetting);

                default:
                    return string.Empty;
            }
        }
    }
}