namespace Aurora.Framework.Settings
{
    public class AuroraAttributeValue
    {
        public BooleanAttributeValue BooleanValue { get; set; }
        public IntegerAttributeValue IntegerValue { get; set; }
        public MoneyAttributeValue MoneyValue { get; set; }
        public NumericAttributeValue NumericValue { get; set; }
        public OptionsListAttributeValue OptionsListValue { get; set; }
        public TextAttributeValue TextValue { get; set; }

        public string GetValueString(AuroraAttributeSetting setting)
        {
            switch (setting.DataType)
            {
                case AuroraDataType.Boolean:
                    if (BooleanValue == null) throw new PlatformException(ExceptionMessages.InvalidBooleanAttributeSetting);
                    return BooleanValue.GetValueWrapper();

                case AuroraDataType.Integer:
                    if (IntegerValue == null) throw new PlatformException(ExceptionMessages.InvalidIntegerAttributeSetting);
                    return IntegerValue.GetValueWrapper(setting.IntegerSetting);

                case AuroraDataType.Money:
                    if (MoneyValue == null) throw new PlatformException(ExceptionMessages.InvalidMoneyAttributeSetting);
                    return MoneyValue.GetValueWrapper(setting.MoneySetting);

                case AuroraDataType.Numeric:
                    if (NumericValue == null) throw new PlatformException(ExceptionMessages.InvalidNumericAttributeSetting);
                    return NumericValue.GetValueWrapper(setting.NumericSetting);

                case AuroraDataType.OptionsList:
                    if (OptionsListValue == null) throw new PlatformException(ExceptionMessages.InvalidOptionsListAttributeSetting);
                    return OptionsListValue.GetValueWrapper(setting.OptionsListSetting);

                case AuroraDataType.Text:
                    if (TextValue == null) throw new PlatformException(ExceptionMessages.InvalidTextAttributeSetting);
                    return TextValue.GetValueWrapper(setting.TextSetting);

                default: return null;
            }
        }
    }
}