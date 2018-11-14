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

        public static string GetCompletedPlanDate(DateTime pDate1, DateTime pDate2)
        {
            TextInfo textInfo = CultureInfo.CurrentCulture.TextInfo;
            if ( pDate1.Date == pDate2.Date )
            {
                return textInfo.ToTitleCase( pDate1.ToString( "dddd", CultureInfo.CurrentCulture )) + ", " + textInfo.ToTitleCase( pDate1.ToString( "MMMM", CultureInfo.CurrentCulture )) + " " + pDate1.Day + " · " + pDate1.ToString( "HH:mm" ) + " - " + pDate2.ToString( "HH:mm" );
            }
            else
            {
                return textInfo.ToTitleCase( pDate1.ToString( "MMMM", CultureInfo.CurrentCulture )) + " " + pDate1.Day + " - " + pDate2.Day + " · " + pDate1.ToString( "HH:mm" ) + " - " + pDate2.ToString( "HH:mm" );
            }
        }
    }
}
