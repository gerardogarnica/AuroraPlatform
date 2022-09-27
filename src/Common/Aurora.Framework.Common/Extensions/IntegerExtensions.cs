using Aurora.Framework.Exceptions;

namespace Aurora.Framework
{
    public static class IntegerExtensions
    {
        public static bool IsIntoInterval(this int value, int minValue, int maxValue)
        {
            if (maxValue < minValue)
            {
                throw new PlatformException(string.Format(ExceptionMessages.OutOfRangeException, minValue, maxValue));
            }

            return value >= minValue && value <= maxValue;
        }

        public static string PadZero(this int value, int length)
        {
            return value.ToString().PadZero(length);
        }
    }
}