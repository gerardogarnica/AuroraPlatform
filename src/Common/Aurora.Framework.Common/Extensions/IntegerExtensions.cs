namespace Aurora.Framework
{
    public static class IntegerExtensions
    {
        public static bool IsIntoInterval(this int value, int minValue, int maxValue)
        {
            if (maxValue < minValue)
            {
                // TODO: add exception
                //throw new Exceptions.PlatformException(ExceptionMessages.OutOfRangeException);
            }

            return value >= minValue && value <= maxValue;
        }

        public static string PadZero(this int value, int length)
        {
            return value.ToString().PadZero(length);
        }
    }
}