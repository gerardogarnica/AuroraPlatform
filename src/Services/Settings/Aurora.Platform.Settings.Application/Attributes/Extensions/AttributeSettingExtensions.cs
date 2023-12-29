using Aurora.Framework;
using Aurora.Framework.Settings;
using Aurora.Platform.Settings.Domain.Entities;
using AttributeSettingModel = Aurora.Framework.Platform.Attributes.AttributeSetting;

namespace Aurora.Platform.Settings.Application.Attributes
{
    public static class AttributeSettingExtensions
    {
        public static AttributeValue GetDefaultAttributeValue(this AttributeSetting setting, AttributeSettingModel settingModel, int relationshipId)
        {
            return new AttributeValue()
            {
                Id = settingModel.AttributeId,
                RelationshipId = relationshipId,
                Value = settingModel.GetDefaultValue(),
                AttributeSetting = setting
            };
        }

        private static string GetDefaultValue(this AttributeSettingModel setting)
        {
            switch (setting.DataType)
            {
                case AuroraDataType.Boolean:
                    var booleanValue = new BooleanAttributeValue()
                    {
                        Value = setting.BooleanSetting.DefaultValue
                    };
                    return booleanValue.GetValueWrapper();

                case AuroraDataType.Integer:
                    var integerValue = new IntegerAttributeValue()
                    {
                        Value = setting.IntegerSetting.DefaultValue
                    };
                    return integerValue.GetValueWrapper(setting.IntegerSetting);

                case AuroraDataType.Money:
                    var moneyValue = new MoneyAttributeValue()
                    {
                        Value = setting.MoneySetting.DefaultValue
                    };
                    return moneyValue.GetValueWrapper(setting.MoneySetting);

                case AuroraDataType.Numeric:
                    var numericValue = new NumericAttributeValue()
                    {
                        Value = setting.NumericSetting.DefaultValue
                    };
                    return numericValue.GetValueWrapper(setting.NumericSetting);

                case AuroraDataType.Options:
                    var optionListValue = new OptionsAttributeValue()
                    {
                        ItemCodes = setting.OptionsSetting.DefaultItemCodes
                    };
                    return optionListValue.GetValueWrapper(setting.OptionsSetting);

                case AuroraDataType.Text:
                    var textValue = new TextAttributeValue()
                    {
                        Value = setting.TextSetting.DefaultValue
                    };
                    return textValue.GetValueWrapper(setting.TextSetting);

                default:
                    return string.Empty;
            }
        }
    }
}