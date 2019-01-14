using Joinup.Utils;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Joinup.Helpers
{
    public class MailHelper
    {
        public static async Task SendEmail(string pToEmail, string pSubject, string pBody)
        {
            try
            {

                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("joinupcompany@gmail.com");
                mail.To.Add(pToEmail);
                mail.Subject = pSubject;
                mail.Body = pBody;

                SmtpServer.Port = 587;
                SmtpServer.Host = "smtp.gmail.com";
                SmtpServer.EnableSsl = true;
                SmtpServer.UseDefaultCredentials = false;
                SmtpServer.Credentials = new System.Net.NetworkCredential("joinupcompany@gmail.com", "Joinup_92");

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                ToastNotificationUtils.ShowErrorToastNotifications("Ha habido un error al enviar el correo. Intentelo de nuevo más tarde");
            }
        }
    }
}
