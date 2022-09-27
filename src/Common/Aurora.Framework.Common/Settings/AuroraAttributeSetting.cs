using Aurora.Framework.Exceptions;

namespace Aurora.Framework.Settings
{
    public class AuroraAttributeSetting
    {
        public AuroraDataType DataType { get; set; }
        public BooleanAttributeSetting BooleanSetting { get; set; }
        public IntegerAttributeSetting IntegerSetting { get; set; }
        public MoneyAttributeSetting MoneySetting { get; set; }
        public NumericAttributeSetting NumericSetting { get; set; }
        public OptionsListAttributeSetting OptionsListSetting { get; set; }
        public TextAttributeSetting TextSetting { get; set; }

        public string GetConfigurationSetting()
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

                case AuroraDataType.OptionsList:
                    if (OptionsListSetting == null) throw new PlatformException(ExceptionMessages.InvalidOptionsListAttributeSetting);
                    return OptionsListSetting.GetSettingWrapper();

                case AuroraDataType.Text:
                    if (TextSetting == null) throw new PlatformException(ExceptionMessages.InvalidTextAttributeSetting);
                    return TextSetting.GetSettingWrapper();

                default: return null;
            }
        }
    }
}