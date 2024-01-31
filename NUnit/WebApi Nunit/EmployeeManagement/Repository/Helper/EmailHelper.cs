using EmployeeManagement.Models.Application_Models;
using EmployeeManagement.Models.DbModels;
using EmployeeManagement.Repository.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Serilog;
using System.Net;

namespace EmployeeManagement.Repository.Helper
{
    public class EmailHelper : IEmailService
    {
        private readonly EmailConfig _config;
        public EmailHelper(IOptions<EmailConfig> config)
        {
            this._config = config.Value;
        }

        public async Task SendEmail(EmailRequest mailContent)
        {
            try
            {
                var email = new MimeMessage();

                email.Sender = MailboxAddress.Parse(_config.Sender);
                email.To.Add(MailboxAddress.Parse(mailContent.ToEmailAddress));
                email.Subject = mailContent.Subject;
                var builder = new BodyBuilder();


                builder.HtmlBody = mailContent.Body;

                email.Body = builder.ToMessageBody();

                using (var smtp = new SmtpClient())
                {
                    await smtp.ConnectAsync(_config.Host, _config.Port, SecureSocketOptions.StartTls);

                    await smtp.AuthenticateAsync(_config.Sender, _config.Password);

                    await smtp.SendAsync(email);

                    await smtp.DisconnectAsync(true);
                }
            }
            catch (Exception error)
            {
                Log.Error("{@error}", error);
            }
        }
    }
}
