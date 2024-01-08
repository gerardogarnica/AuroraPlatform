using System.Globalization;

namespace Aurora.Framework
{
    public static class StringExtensions
    {
        public static string GetLastSplit(this string value, string separator)
        {
            var stringSeparator = new string[] { separator };
            var tokens = value.Split(stringSeparator, StringSplitOptions.RemoveEmptyEntries);

            return tokens[tokens.Length - 1];
        }

        public static string PadZero(this string value, int length)
        {
            var newValue = value.PadLeft(length, '0');

            if (newValue.Length > length)
            {
                return newValue.Substring(newValue.Length - length, length);
            }

            return newValue;
        }

        public static bool? ToBoolean(this string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                var booleanTrueStrings = new string[] { "S", "SI", "V", "VERDADERO", "Y", "YES", "T", "TRUE", "1" };
                var booleanFalseStrings = new string[] { "N", "NO", "F", "FALSE", "F", "FALSE", "0" };

                if (Array.IndexOf(booleanTrueStrings, value.Trim().ToUpper()) > -1) return true;
                if (Array.IndexOf(booleanFalseStrings, value.Trim().ToUpper()) > -1) return false;
            }

            return null;
        }

        public static DateTime? ToDateTime(this string value, string format)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                if (DateTime.TryParseExact(value, format, null, DateTimeStyles.None, out var returnDate))
                    return returnDate;
            }

            return null;
        }

        public static string Truncate(this string value, int length)
        {
            return value.Length > length ? value.Substring(0, length) : value;
        }
    }
}