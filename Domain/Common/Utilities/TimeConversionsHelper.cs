namespace Domain.Common.Utilities
{
    public static class TimeConversionsHelper
    {
        public static int DaysToSeconds(this int Days)
        {
            return Days * 24 * 60 * 60;
        }
        public static int HoursToSeconds(this int Hours)
        {
            return Hours * 60 * 60;
        }
        public static int MinutesToSeconds(this int Minutes)
        {
            return Minutes * 60;
        }
        public static int ToDays(this int Seconds)
        {
            return Seconds / 60 / 60 / 24;
        }
        public static int ToHours(this int Seconds)
        {
            return Seconds / 60 / 60;
        }
        public static int ToMinutes(this int Seconds)
        {
            return Seconds / 60;
        }
        public static TimeType ScaleBackToTimeType(this int Seconds, out int value)
        {
            value = 0;
            value = Seconds.ToMinutes();
            if (value >= 60)
            {
                value = Seconds.ToHours();
                if (value >= 24)
                {
                    value = Seconds.ToDays();
                    return TimeType.Days;
                }
                else
                {
                    return TimeType.Hours;
                }
            }
            else
            {
                return TimeType.Minutes;
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
