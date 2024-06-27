using InfoDev.IOffice.ICommon.Results;
using InfoDev.IOffice.Models.Email;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InfoDev.IOffice.Infrastructure.Interface.Others
{
    public interface IEmailSender
    {
        Task SendEmailAsync(string email, string subject, string message);
        Task<DataResult> SendEmailProcAsync(EmailLog model);
        Task<EmailLog> SendEmailWithAttachmentProcAsync(EmailLog model);

        Task<EmailLog> RetrySendEmailWithAttachmentProcAsync(EmailLog model);
        Task<DataResult> AttachmentAsync(string ToMail,string subject, string content,byte[] File,string extension);

    }
}
