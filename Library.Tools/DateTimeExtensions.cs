using System;
using System.Globalization;

namespace Library.Tools
{
    public static class DateTimeExtensions
    {
        public static int GetIso8601WeekOfYear(this DateTime datetime)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(datetime);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                datetime = datetime.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(datetime, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }

        public static DateTime GetFirstDayOfWeek(this DateTime datetime, CultureInfo cultureInfo)
        {
            DateTime now = datetime.Date;
            DateTime begining;

            if (cultureInfo == null)
                throw new ArgumentNullException("cultureInfo");
                
            var firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;

            int offset = firstDayOfWeek - now.DayOfWeek;
            if (offset != 1)
            {
                DateTime weekStart = now.AddDays(offset);
                begining = weekStart;
            }
            else
            {
                begining = now.AddDays(-6);
            }

            return begining;
        }

        public static DateTime GetLastDayOfWeek(this DateTime datetime, CultureInfo cultureInfo)
        {
            DateTime now = datetime.Date;
            DateTime end;

            if (cultureInfo == null)
                throw new ArgumentNullException("cultureInfo");

            var firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;

            int offset = firstDayOfWeek - now.DayOfWeek;
            if (offset != 1)
            {
                DateTime weekStart = now.AddDays(offset);
                DateTime endOfWeek = weekStart.AddDays(6);
                end = endOfWeek;
            }
            else
            {
                end = now;
            }

            return end;
        }

        public static void GetWeek(this DateTime datetime, CultureInfo cultureInfo, out DateTime begining, out DateTime end)
        {
            DateTime now = datetime.Date;

            if (cultureInfo == null)
                throw new ArgumentNullException("cultureInfo");

            var firstDayOfWeek = cultureInfo.DateTimeFormat.FirstDayOfWeek;

            int offset = firstDayOfWeek - now.DayOfWeek;
            if (offset != 1)
            {
                DateTime weekStart = now.AddDays(offset);
                DateTime endOfWeek = weekStart.AddDays(6);
                begining = weekStart;
                end = endOfWeek;
            }
            else
            {
                begining = now.AddDays(-6);
                end = now;
            }
        }
    }
}
