using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FindInCn.Shared.Helpers
{
    public class MailHelper
    {
        public static void SendPasswd(string address, string key)
        {
#if DEBUG
            var thread = new Thread(new ParameterizedThreadStart(param => { System.Windows.Forms.Clipboard.SetText(key); }));
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
#endif
            SendEmail(address, "FindInCn -- Login", $"Your temporary password is \"{key}\"");
        }

        static bool SendEmail(string to, string subject, string body)
        {
            try
            {
                var mailMessage = new MailMessage("root098@gmail.com", to) { Subject = subject, Body = body, IsBodyHtml = true };
                SmtpClient smtp;
                // in dev we use gmail to send emails, gmail is using SSL
                smtp = new SmtpClient { EnableSsl = true };
                smtp.Credentials = new NetworkCredential("root098@gmail.com", ConfigurationHelper.MailPassword);
                smtp.Host = "smtp.gmail.com";
                smtp.Send(mailMessage);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
