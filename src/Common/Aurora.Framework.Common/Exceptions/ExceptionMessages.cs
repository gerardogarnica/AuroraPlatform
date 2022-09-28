namespace Aurora.Framework.Exceptions
{
    internal static class ExceptionMessages
    {
        internal const string InvalidBooleanAttributeSetting = "Boolean type attribute not set.";
        internal const string InvalidDefaultValueAttributeSetting = "The default value '{0}' must be in the range between the minimum value '{1}' and the maximum value '{2}'.";
        internal const string InvalidIntegerAttributeSetting = "Integer type attribute not set.";
        internal const string InvalidLengthAttributeValue = "Attribute value '{0}' must be between '{1}' and '{2}' characters.";
        internal const string InvalidLengthDefaultValueAttributeSetting = "The default value '{0}' must be between '{1}' and '{2}' characters.";
        internal const string InvalidLengthRangeValueAttributeSetting = "The maximum length '{1}' can not be less than the minimum length '{0}'.";
        internal const string InvalidMoneyAttributeSetting = "Money type attribute not set.";
        internal const string InvalidNumericAttributeSetting = "Numeric type attribute not set.";
        internal const string InvalidOptionsListAttributeSetting = "Options list type attribute not set.";
        internal const string InvalidOptionsListItemAttributeValue = "Item count cannot be greater than '{0}' items.";
        internal const string InvalidPatternAttributeValue = "Attribute value '{0}' does not match pattern '{1}'.";
        internal const string InvalidPatternValueAttributeSetting = "Attribute default value '{0}' does not match pattern '{1}'.";
        internal const string InvalidRangeAttributeValue = "The value '{0}' must be in the range between the minimum value '{1}' and the maximum value '{2}'.";
        internal const string InvalidRangeValueAttributeSetting = "The maximum value '{1}' can not be less than the minimum value '{0}'.";
        internal const string InvalidTextAttributeSetting = "Text type attribute not set.";
        internal const string OutOfRangeException = "The maximum value '{1}' can not be less than the minimum value '{0}'.";
    }
}