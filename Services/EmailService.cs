using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Services
{
    public class EmailService
    {
        public bool Send(string toName,
                         string toEmail,
                         string subject,
                         string body,
                         string fromName = "Suporte 4Sec Global",
                         string fromEmail = "contato@4secglobal.com")
        {
            SmtpClient smtpClient = new(Configuration.Smtp.Host, Configuration.Smtp.Port);
            smtpClient.Credentials = new NetworkCredential(Configuration.Smtp.UserName, Configuration.Smtp.Password);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;

            var mail = new MailMessage();

            mail.From = new MailAddress(fromEmail, fromName);
            mail.To.Add(new MailAddress(toEmail, toName));
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            try
            {
                smtpClient.Send(mail);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
