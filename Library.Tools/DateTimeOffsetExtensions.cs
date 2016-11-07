using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Tools
{
    public static class DateTimeOffsetExtensions
    {
        // This presumes that weeks start with Monday.
        // Week 1 is the 1st week of the year with a Thursday in it.
        public static int GetIso8601WeekOfYear(this DateTimeOffset datetimeOffset)
        {
            return DateTimeExtensions.GetIso8601WeekOfYear(datetimeOffset.DateTime);
        }

        public static DateTime GetFirstDayOfWeek(this DateTimeOffset datetimeOffset, CultureInfo cultureInfo)
        {
            return DateTimeExtensions.GetFirstDayOfWeek(datetimeOffset.DateTime, cultureInfo);
        }

        public static DateTime GetLastDayOfWeek(this DateTimeOffset datetimeOffset, CultureInfo cultureInfo)
        {
            return DateTimeExtensions.GetLastDayOfWeek(datetimeOffset.DateTime, cultureInfo);
        }

        public static void GetWeek(this DateTimeOffset datetimeOffset, CultureInfo cultureInfo, out DateTime begining, out DateTime end)
        {
            DateTimeExtensions.GetWeek(datetimeOffset.DateTime, cultureInfo, out begining, out end);
        }
    }
}
