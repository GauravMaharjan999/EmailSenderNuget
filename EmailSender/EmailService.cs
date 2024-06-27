using InfoDev.IOffice.ICommon.Enums;
using InfoDev.IOffice.ICommon.Others;
using InfoDev.IOffice.ICommon.Results;
using InfoDev.IOffice.Infrastructure.Interface.Others;
using InfoDev.IOffice.Models.Email;
using MimeKit;
using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace InfoDev.IOffice.Helpers
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class EmailSender : IEmailSender
    {
        private readonly MailSettings _mailSettings;
        //private readonly IMailSettingsBL _mailServerSettingBL;
        public EmailSender(MailSettings mailSettings/*, IMailSettingsBL mailServerSettingBL*/)
        {
            _mailSettings = mailSettings;
            //_mailServerSettingBL = mailServerSettingBL;
        }

        //public async Task SendEmailAsync(string toemail, string subject, string message, bool isHTMLcontent = true)
        //{

        //    if (toemail == null) return;  
        //    try
        //    {
        //        ////-< setup Email >-
        //        //MailMessage email = new MailMessage();
        //        //email.To.Add(new MailAddress(toemail));
        //        //email.From = new MailAddress(_mailSettings.Mail, "Mailservice");
        //        //email.Subject = _mailSettings.DisplayName + " " + subject;
        //        //email.Body = message;
        //        //email.IsBodyHtml = true;

        //        ////-</ setup Email >-
        //        ////-< EmailClient >-
        //        //SmtpClient smtp = new SmtpClient();
        //        //smtp.Host = _mailSettings.Host;
        //        //smtp.Port = _mailSettings.Port;
        //        //smtp.Credentials = new NetworkCredential(_mailSettings.Mail, _mailSettings.Password);
        //        //smtp.EnableSsl = true;
        //        ////-</ EmailClient >-



        //        //await smtp.SendMailAsync(email);



        //        #region New MIME Kit config
        //        var emailToSend = new MimeMessage();
        //        emailToSend.From.Add(MailboxAddress.Parse(_mailSettings.Mail));
        //        emailToSend.To.Add(MailboxAddress.Parse(toemail));
        //        emailToSend.Subject = subject;
        //        emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message };
        //        using (var emailClient = new MailKit.Net.Smtp.SmtpClient())
        //        {
        //            emailClient.Connect(_mailSettings.Host, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
        //            emailClient.Authenticate(_mailSettings.Mail, _mailSettings.Password);
        //            emailClient.Send(emailToSend);
        //            emailClient.Disconnect(true);
        //        }
        //        //return Task.CompletedTask;    
        //        #endregion


        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error EmailSender.cs error:" + ex.InnerException);
        //    }
        //}
        public async Task SendEmailAsync(string toemail, string subject, string message, bool isHTMLcontent = true)
        {
            if (toemail == null) return;

            try
            {
                var emailToSend = new MimeMessage();
                emailToSend.From.Add(MailboxAddress.Parse(_mailSettings.Mail));
                emailToSend.To.Add(MailboxAddress.Parse(toemail));
                emailToSend.Subject = _mailSettings.DisplayName + " " + subject;

                if (isHTMLcontent)
                {
                    emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = message };
                }
                else
                {
                    emailToSend.Body = new TextPart(MimeKit.Text.TextFormat.Plain) { Text = message };
                }

                using (var emailClient = new SmtpClient())
                {
                    await emailClient.ConnectAsync(_mailSettings.Host, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                    await emailClient.AuthenticateAsync(_mailSettings.Mail, _mailSettings.Password);
                    await emailClient.SendAsync(emailToSend);
                    await emailClient.DisconnectAsync(true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error EmailSender.cs error:" + ex.InnerException?.Message ?? ex.Message);
            }


            public async Task<DataResult> SendEmailProcAsync(EmailLog model)
        {

            if (model.EmailTo == null) return new DataResult() { Message = "Email to is necessary", ResultType = ResultType.Failed };
            else if (model.EmailFrom == null) return new DataResult() { Message = "Email From is necessary", ResultType = ResultType.Failed };
            try
            {
                //-< setup Email >-
                //var mailServerSettings = await _mailServerSettingBL.GetDefaultMailSettings();
                //_mailSettings.Host = mailServerSettings.MailServerSMTP;
                //_mailSettings.Mail = mailServerSettings.EmailAddress;
                //_mailSettings.DisplayName = mailServerSettings.DisplayName;
                //_mailSettings.Password = mailServerSettings.Password;
                //_mailSettings.Port = mailServerSettings.SMTPPort;
                MailMessage email = new MailMessage();
                email.To.Add(new MailAddress(model.EmailTo));
                email.From = new MailAddress(model.EmailFrom, "Mailservice");
                email.Subject = _mailSettings.DisplayName + " - " + model.Subject;
                email.Body = model.Content;
                email.IsBodyHtml = true;

                //-</ setup Email >-
                //-< EmailClient >-
                SmtpClient smtp = new SmtpClient();
                smtp.Host = _mailSettings.Host;
                smtp.Port = _mailSettings.Port;
                smtp.Credentials = new NetworkCredential(_mailSettings.Mail, _mailSettings.Password);
                smtp.EnableSsl = true;
                //-</ EmailClient >-

                await smtp.SendMailAsync(email);
                return new DataResult() { Message = "Email send Sucessfully", ResultType = ResultType.Success};
            }
            catch (Exception ex)
            {
                return new DataResult() { Message = "Error Occured - " + ex.InnerException.ToString(), ResultType = ResultType.Success };
            }
        }




        public async Task<EmailLog> SendEmailWithAttachmentProcAsync(EmailLog model)
        {

          
            try
            {
                //var mailServerSettings = await _mailServerSettingBL.GetDefaultMailSettings();
                //_mailSettings.Host = mailServerSettings.MailServerSMTP;
                //_mailSettings.Mail = mailServerSettings.EmailAddress;
                //_mailSettings.DisplayName = mailServerSettings.DisplayName;
                //_mailSettings.Password = mailServerSettings.Password;
                //_mailSettings.Port = mailServerSettings.SMTPPort;
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(_mailSettings.Host);
                mail.From = new MailAddress(_mailSettings.Mail,_mailSettings.DisplayName);
                mail.To.Add(model.EmailTo);
                mail.Headers.Add("Disposition-Notification-To", _mailSettings.Mail);
                mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;
                mail.Subject = _mailSettings.DisplayName + " - " + model.Subject;
                mail.Body = model.Content;
                mail.IsBodyHtml = true;
                
                // loop all the uploaded files
                if (model.EmailAttachmentDoc != null)
                {
                    foreach (var file in model.EmailAttachmentDoc)
                    {
                        //add the file from the fileupload as an attachment
                        mail.Attachments.Add(new Attachment(new MemoryStream(file.File), file.FileName, MediaTypeNames.Application.Octet));
                    }
                }

                SmtpServer.Port = _mailSettings.Port;
                SmtpServer.Credentials = new System.Net.NetworkCredential(_mailSettings.Mail, _mailSettings.Password);
                SmtpServer.EnableSsl = true;

               await SmtpServer.SendMailAsync(mail);
                model.RetryCount = model.RetryCount;
                model.EmailStatus = EmailStatus.Sent;
                model.Remarks = "Email sent Sucessfully";


                return model;
            }
            catch (Exception ex)
            {
                model.RetryCount = model.RetryCount;
                model.EmailStatus = EmailStatus.Failed;
                if (ex.InnerException != null)
                {
                    model.Remarks = "Error Occured - " + ex.InnerException.ToString();
                }
                return model;
            }
        }

        public async Task<EmailLog> RetrySendEmailWithAttachmentProcAsync(EmailLog model)
        {


            try
            {
                //var mailServerSettings = await _mailServerSettingBL.GetDefaultMailSettings();
                //_mailSettings.Host = mailServerSettings.MailServerSMTP;
                //_mailSettings.Mail = mailServerSettings.EmailAddress;
                //_mailSettings.DisplayName = mailServerSettings.DisplayName;
                //_mailSettings.Password = mailServerSettings.Password;
                //_mailSettings.Port = mailServerSettings.SMTPPort;
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(_mailSettings.Host);
                mail.From = new MailAddress(_mailSettings.Mail);
                mail.To.Add(model.EmailTo);
                mail.Subject = _mailSettings.DisplayName + " - " + model.Subject;
                mail.Body = model.Content;
                mail.IsBodyHtml = true;

                // loop all the uploaded files
                if (model.EmailAttachmentDoc != null)
                {
                    foreach (var file in model.EmailAttachmentDoc)
                    {
                        //add the file from the fileupload as an attachment
                        mail.Attachments.Add(new Attachment(new MemoryStream(file.File), file.FileName, MediaTypeNames.Application.Octet));
                    }
                }

                SmtpServer.Port = _mailSettings.Port;
                SmtpServer.Credentials = new System.Net.NetworkCredential(_mailSettings.Mail, _mailSettings.Password);
                SmtpServer.EnableSsl = true;

                await SmtpServer.SendMailAsync(mail);
                model.RetryCount = ++model.RetryCount;
                model.EmailStatus = EmailStatus.Sent;
                model.Remarks = "Email sent Sucessfully";


                return model;
            }
            catch (Exception ex)
            {
                model.RetryCount = ++model.RetryCount;
                model.EmailStatus = EmailStatus.Failed;
                model.Remarks = "Error Occured - " + ex.InnerException.ToString();
                return model;
            }
        }


        public async Task<DataResult> AttachmentAsync(string EmailTo,string Subject, string Content, byte[] File, string extension)
        {
            if (EmailTo == null) return new DataResult() { Message = "Email to is necessary", ResultType = ResultType.Failed };
            try
            {
                //var mailServerSettings = await _mailServerSettingBL.GetDefaultMailSettings();
                //_mailSettings.Host = mailServerSettings.MailServerSMTP;
                //_mailSettings.Mail = mailServerSettings.EmailAddress;
                //_mailSettings.DisplayName = mailServerSettings.DisplayName;
                //_mailSettings.Password = mailServerSettings.Password;
                //_mailSettings.Port = mailServerSettings.SMTPPort;
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(_mailSettings.Host);
                mail.From = new MailAddress(_mailSettings.Mail);
                mail.To.Add(EmailTo);
                mail.Subject = _mailSettings.DisplayName + " - " + Subject;
                mail.Body = Content;
                mail.IsBodyHtml = true;

                // loop all the uploaded files
                if (File != null)
                {
                    //add the file from the fileupload as an attachment
                    mail.Attachments.Add(new Attachment(new MemoryStream(File), "AppointmentLetter", extension));   
                }

                SmtpServer.Port = _mailSettings.Port;
                SmtpServer.Credentials = new System.Net.NetworkCredential(_mailSettings.Mail, _mailSettings.Password);
                SmtpServer.EnableSsl = true;

                await SmtpServer.SendMailAsync(mail);
                return new DataResult() { Message = "Email send Sucessfully", ResultType = ResultType.Success };
            }
            catch (Exception ex)
            {
                return new DataResult() { Message = "Error Occured - " + ex.InnerException.ToString(), ResultType = ResultType.Success };
            }
        }
      

    }
}
