using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackAgain.Service
{
    public interface IEmailService
    {
        bool SendEMailAsync(string mailFrom, string mailFromPassword, string mailTo, string content, string subject, bool isBodyHtml);
        string ComposeConfirmationMail(string email, string url);
    }
}
