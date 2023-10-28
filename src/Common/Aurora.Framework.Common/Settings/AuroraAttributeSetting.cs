namespace Aurora.Framework.Settings
{
    public class AuroraAttributeSetting
    {
        public AuroraDataType DataType { get; set; }
        public BooleanAttributeSetting BooleanSetting { get; set; }
        public IntegerAttributeSetting IntegerSetting { get; set; }
        public MoneyAttributeSetting MoneySetting { get; set; }
        public NumericAttributeSetting NumericSetting { get; set; }
        public OptionsAttributeSetting OptionsSetting { get; set; }
        public TextAttributeSetting TextSetting { get; set; }

        public string GetSettingString()
        {
            switch (DataType)
            {
                case AuroraDataType.Boolean:
                    if (BooleanSetting == null) throw new PlatformException(ExceptionMessages.InvalidBooleanAttributeSetting);
                    return BooleanSetting.GetSettingWrapper();

                case AuroraDataType.Integer:
                    if (IntegerSetting == null) throw new PlatformException(ExceptionMessages.InvalidIntegerAttributeSetting);
                    return IntegerSetting.GetSettingWrapper();

                case AuroraDataType.Money:
                    if (MoneySetting == null) throw new PlatformException(ExceptionMessages.InvalidMoneyAttributeSetting);
                    return MoneySetting.GetSettingWrapper();

                case AuroraDataType.Numeric:
                    if (NumericSetting == null) throw new PlatformException(ExceptionMessages.InvalidNumericAttributeSetting);
                    return NumericSetting.GetSettingWrapper();

                case AuroraDataType.Options:
                    if (OptionsSetting == null) throw new PlatformException(ExceptionMessages.InvalidOptionsAttributeSetting);
                    return OptionsSetting.GetSettingWrapper();

                case AuroraDataType.Text:
                    if (TextSetting == null) throw new PlatformException(ExceptionMessages.InvalidTextAttributeSetting);
                    return TextSetting.GetSettingWrapper();

                default: return null;
            }
        }
    }
}