using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Joinup.Helpers
{
    public class RegexHelper
    {
        public static bool IsValidEmail(string pEmail)
        {
            try
            {
                var email = new MailAddress(pEmail);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        public static bool IsValidUrl(string pUrl)
        {
            try
            {
                Uri uriResult;
                bool result = Uri.TryCreate( pUrl, UriKind.Absolute, out uriResult )
                    && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
                return result;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
