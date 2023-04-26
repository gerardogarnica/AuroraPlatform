namespace Aurora.Framework
{
    public static class DecimalExtensions
    {
        public static bool IsIntoInterval(this decimal value, decimal minValue, decimal maxValue)
        {
            if (maxValue < minValue)
            {
                throw new PlatformException(string.Format(ExceptionMessages.OutOfRangeException, minValue, maxValue));
            }

            return value >= minValue && value <= maxValue;
        }

        public static decimal Round(this decimal value, int decimals = 2)
        {
            return Math.Round(value, decimals, MidpointRounding.AwayFromZero);
        }

        public static string ToCurrency(this decimal value)
        {
            value = value.Round(2);

            return string.Format("$ {0}", value);
        }
    }
}