namespace Domain.Common.Extensions
{
    public static class TimeConversionsHelper
    {
        public static long GetDifferenceInMonths(DateTime StartDate, DateTime EndDate)
        {
            var days = from day in StartDate.DaysInRangeUntil(EndDate)
                       let start = Max(day.AddHours(7), StartDate)
                       let end = Min(day.AddHours(19), EndDate)
                       select (end - start).TotalDays;

            return (long)Math.Round(days.Sum() / 30);
        }
        public static IEnumerable<DateTime> DaysInRangeUntil(this DateTime start, DateTime end)
        {
            return Enumerable.Range(0, 1 + (int)(end.Date - start.Date).TotalDays)
                             .Select(dt => start.Date.AddDays(dt));
        }
        public static bool IsWeekendDay(this DateTime dt)
        {
            return dt.DayOfWeek == DayOfWeek.Saturday
                || dt.DayOfWeek == DayOfWeek.Sunday;
        }
        public static DateTime Max(DateTime start, DateTime end)
        {
            return new DateTime(Math.Max(start.Ticks, end.Ticks));
        }
        public static DateTime Min(DateTime start, DateTime end)
        {
            return new DateTime(Math.Min(start.Ticks, end.Ticks));
        }

        public static int ToSeconds(this int value, TimeType valueType)
        {
            switch (valueType)
            {
                case TimeType.Minutes:
                    return value * 60;
                case TimeType.Hours:
                    return value * 60 * 60;
                case TimeType.Days:
                    return value * 24 * 60 * 60;
                default:
                    return value;
            }
        }
        public static int ToMinutes(this int value, TimeType valueType)
        {
            switch (valueType)
            {
                case TimeType.Seconds:
                    return value / 60;
                case TimeType.Hours:
                    return value * 60;
                case TimeType.Days:
                    return value * 24 * 60;
                default:
                    return value;
            }
        }
        public static int ToDays(this int value, TimeType valueType)
        {
            switch (valueType)
            {
                case TimeType.Seconds:
                    return value / 60 / 60 / 24;
                case TimeType.Minutes:
                    return value / 60 / 24;
                case TimeType.Hours:
                    return value / 24;
                default:
                    return value;
            }
        }
        public static int ToHours(this int value, TimeType valueType)
        {
            switch (valueType)
            {
                case TimeType.Seconds:
                    return value / 60 / 60;
                case TimeType.Minutes:
                    return value / 60;
                case TimeType.Days:
                    return value * 24;
                default:
                    return value;
            }
        }
        public static long ToMinutes(this long value, TimeType valueType)
        {
            switch (valueType)
            {
                case TimeType.Seconds:
                    return value / 60;
                case TimeType.Hours:
                    return value * 60;
                case TimeType.Days:
                    return value * 24 * 60;
                default:
                    return value;
            }
        }
        public static long ToDays(this long value, TimeType valueType)
        {
            switch (valueType)
            {
                case TimeType.Seconds:
                    return value / 60 / 60 / 24;
                case TimeType.Minutes:
                    return value / 60 / 24;
                case TimeType.Hours:
                    return value / 24;
                default:
                    return value;
            }
        }
        public static long ToHours(this long value, TimeType valueType)
        {
            switch (valueType)
            {
                case TimeType.Seconds:
                    return value / 60 / 60;
                case TimeType.Minutes:
                    return value / 60;
                case TimeType.Days:
                    return value * 24;
                default:
                    return value;
            }
        }
    }
    public enum TimeType
    {
        Days = 1,
        Hours = 2,
        Minutes = 3,
        Seconds = 4,
    }
}
