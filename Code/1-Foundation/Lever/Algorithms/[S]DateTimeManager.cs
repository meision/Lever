using System;

namespace Meision.Algorithms
{
    public static class DateTimeManager
    {
        #region TimeSpan
        public static string GetTimeSpanText(int second)
        {
            if (second < 0)
            {
                return "??:??:??";
            }

            return new TimeSpan(0, 0, second).ToString();
        }

        /// <summary>
        /// Display Time span
        /// </summary>
        /// <param name="timezone">Time zone</param>
        public static string DisplayTimezone(double timezone)
        {
            ThrowHelper.ArgumentOutOfRange(((timezone < -12) || (timezone > 12)), nameof(timezone));

            if (timezone == 0)
            {
                return "UTC";
            }

            TimeSpan span = TimeSpan.FromHours(timezone);
            if (timezone > 0)
            {
                return string.Format(
                    System.Globalization.CultureInfo.InvariantCulture,
                    "+{0:d2}:{1:d2}",
                    span.Hours,
                    span.Minutes);
            }
            else
            {
                return string.Format(
                    System.Globalization.CultureInfo.InvariantCulture,
                    "-{0:d2}:{1:d2}",
                    span.Hours,
                    span.Minutes);
            }
        }
        #endregion TimeSpan

        #region Time
        public static DateTime GetBootTime()
        {
            long ticks = (long)(uint)Environment.TickCount;
            DateTime boot = DateTime.Now.AddMilliseconds(-ticks);
            return boot;
        }

        public static string DisplayLocaleTime(DateTime time, double timezone)
        {
            return DisplayLocaleTime(time, timezone, "G");
        }

        public static string DisplayLocaleTime(DateTime time, double timezone, string format)
        {
            string text = string.Format(
                System.Globalization.CultureInfo.InvariantCulture,
                "{0}[{1}]",
                time.AddHours(timezone).ToString(format, System.Globalization.CultureInfo.InvariantCulture),
                (timezone != 0) ? DisplayTimezone(timezone) : "UTC");
            return text;
        }

        public static DateTime ToUtcTime(DateTime time)
        {
            return DateTime.SpecifyKind(time, DateTimeKind.Utc);
        }

        public static DateTime GetSecondPrecision(DateTime time)
        {
            return new DateTime(time.Ticks / UnitTable.SecondToTick * UnitTable.SecondToTick, time.Kind);
        }

        /// <summary>
        /// Get current date time with second precision.
        /// </summary>
        /// <returns></returns>
        public static DateTime GetCurrentDateTimeWithSecondPrecision()
        {
            return GetSecondPrecision(DateTime.Now);
        }

        public static DateTime GetCurrentUtcDateTimeWithSecondPrecision()
        {
            return GetCurrentDateTimeWithSecondPrecision().ToUniversalTime();
        }
        #endregion Time

        public static int GetRemainingTimeout(uint beginTicks, int maxTimeout)
        {
            int timeout;
            if (maxTimeout >= 0)
            {
                uint span = (uint)Environment.TickCount - beginTicks;
                if (maxTimeout > span)
                {
                    timeout = (int)(maxTimeout - span);
                }
                else
                {
                    timeout = 0;
                }
            }
            else
            {
                timeout = -1;
            }

            return timeout;
        }

        private static readonly long UnixTicksBase = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).Ticks;
        public static long ToUnixMillisecond(DateTime time)
        {
            return ((time.Ticks - UnixTicksBase) / TimeSpan.TicksPerMillisecond);
        }
        public static DateTime FromUnixMillisecond(long millisecond)
        {
            return new DateTime(UnixTicksBase + millisecond * TimeSpan.TicksPerMillisecond);
        }

        public static DateTime FirstDateInSameMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, date.Day);
        }

        public static DateTime LastDateInSameMonth(DateTime date)
        {
            return new DateTime(date.Year, date.Month, DateTime.DaysInMonth(date.Year, date.Month));
        }
    }
}
