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
    }
}
