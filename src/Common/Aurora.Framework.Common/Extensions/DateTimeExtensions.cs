using System.Globalization;

namespace Aurora.Framework
{
    public static class DateTimeExtensions
    {
        public static int GetWeekOfYear(this DateTime date, DayOfWeek firstDayOfWeek)
        {
            return new GregorianCalendar().GetWeekOfYear(date, CalendarWeekRule.FirstDay, firstDayOfWeek);
        }

        public static bool IsIntoInterval(this DateTime date, DateTime beginDate, DateTime endDate)
        {
            return date.Date >= beginDate.Date && date.Date <= endDate.Date;
        }

        public static bool IsIntoTimeInterval(this DateTime date, DateTime beginDate, DateTime endDate)
        {
            return date >= beginDate && date <= endDate;
        }

        public static int ToInt32(this DateTime date, DateFormat format)
        {
            return Convert.ToInt32(date.ToString(format));
        }

        public static string ToString(this DateTime date, DateFormat format)
        {
            return date.ToString(format, string.Empty);
        }

        public static string ToString(this DateTime date, DateFormat format, string separator)
        {
            var dayString = date.Day.PadZero(2);
            var monthString = date.Month.PadZero(2);
            var yearString = date.Year.PadZero(4);

            switch (format)
            {
                case DateFormat.DayMonth:
                    return string.Format("{0}{1}{2}", dayString, separator, monthString);

                case DateFormat.DayMonthYear:
                    return string.Format("{0}{1}{2}{3}{4}", dayString, separator, monthString, separator, yearString);

                case DateFormat.MonthDayYear:
                    return string.Format("{0}{1}{2}{3}{4}", monthString, separator, dayString, separator, yearString);

                case DateFormat.MonthYear:
                    return string.Format("{0}{1}{2}", monthString, separator, yearString);

                case DateFormat.YearDayMonth:
                    return string.Format("{0}{1}{2}{3}{4}", yearString, separator, dayString, separator, monthString);

                case DateFormat.YearMonth:
                    return string.Format("{0}{1}{2}", yearString, separator, monthString);

                case DateFormat.YearMonthDay:
                    return string.Format("{0}{1}{2}{3}{4}", yearString, separator, monthString, separator, dayString);

                default: break;
            }

            return string.Empty;
        }

        public static string? ToString(this DateTime? date, DateFormat format)
        {
            return date.ToString(format, string.Empty);
        }

        public static string? ToString(this DateTime? date, DateFormat format, string separator)
        {
            return date.HasValue ? Convert.ToDateTime(date).ToString(format, separator) : null;
        }
    }
}