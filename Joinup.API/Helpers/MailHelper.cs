using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Configuration;

namespace Joinup.API.Helpers
{
    public class MailHelper
    {
        public static async Task SendMail(string pTo, string pSubject, string pBody)
        {
            var message = new MailMessage();
            message.To.Add(new MailAddress(pTo));
            message.From=new MailAddress(WebConfigurationManager.AppSettings["AdminUser"]);
            message.Subject = pSubject;
            message.Body = pBody;
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {
                var credential = new NetworkCredential
                {
                    UserName = WebConfigurationManager.AppSettings["AdminUser"],
                    Password = WebConfigurationManager.AppSettings["AdminPassword"]
                };
                smtp.Credentials = credential;
                smtp.Host = WebConfigurationManager.AppSettings["SMTPName"];
                smtp.Port = int.Parse(WebConfigurationManager.AppSettings["SMTPPort"]);
                smtp.EnableSsl =true;

                await smtp.SendMailAsync(message);
            }
        }
    }
}