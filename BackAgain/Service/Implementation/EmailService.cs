using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace BackAgain.Service
{
    public class EmailService : IEmailService
    {
        public string ComposeConfirmationMail(string Email, string url)
        {
            string confirmationHTML = $"<h1>Please Confirm Your Mail Address</h1>" +
                                      $"<a href={url}>Click here</a>";
            return confirmationHTML;
            
        }

        public bool SendEMailAsync(string mailFrom, string mailFromPassword, string mailTo, string content, string subject, bool isBodyHtml)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.Credentials = new System.Net.NetworkCredential(mailFrom, mailFromPassword);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;

            MailMessage mailMessage = new MailMessage(mailFrom, mailTo);
            mailMessage.Subject = subject;
            mailMessage.Body = content;
            mailMessage.IsBodyHtml = true;

            try
            {
                smtpClient.Send(mailMessage);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
