using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joinup.Common.Helpers
{
    public static class DateTimeHelper
    {
        public static string GetFormattedDayMonth(DateTime pDate)
        {
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;

            return textInfo.ToTitleCase(pDate.Day+" "+ pDate.ToString( "MMMM", CultureInfo.CurrentCulture ));
        }

        public static string GetFormattedHour(DateTime pDate)
        {
            return pDate.ToString( "HH:mm" );
        }
    }
}
