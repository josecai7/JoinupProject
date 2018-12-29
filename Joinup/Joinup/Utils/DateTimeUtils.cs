using System;
using System.Collections.Generic;
using System.Text;

namespace Joinup.Utils
{
    public class DateTimeUtils
    {
        public static string FormatDate(DateTime dt)
        {
            if (dt == DateTime.MinValue)
            {
                return "-";
            }
            else
            {
                return dt.ToString("dd-MM");
            }
        }
        public static string FormatTime(DateTime dt)
        {
            if (dt.TimeOfDay == TimeSpan.MinValue)
            {
                return "-";
            }
            else
            {
                return dt.ToString("HH:mm");
            }
        }
        public static DateTime GetLastWeekDay()
        {
            DateTime date = DateTime.Now;
            while (date.DayOfWeek != DayOfWeek.Sunday)
            {
                date = date.AddDays(1);
            }
            return date;
        }
    }
}
