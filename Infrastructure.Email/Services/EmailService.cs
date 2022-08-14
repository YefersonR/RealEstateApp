using Core.Application.DTOS.Email;
using Core.Application.Inferfaces.Service;
using Core.Domain.Settings;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using MailKit.Security;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Shared.Services
{
    public class EmailService : IEmailService
    {
        private EmailSettings _emailSettings { get; }

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        public async Task Send(EmailRequest request)
        {
            try
            {
                MimeMessage mailmessage = new();
                mailmessage.Sender = MailboxAddress.Parse($"{_emailSettings.EmailFrom}");
                mailmessage.To.Add(MailboxAddress.Parse(request.To));
                mailmessage.Subject = request.Subject;
                BodyBuilder bodyBuilder = new();
                bodyBuilder.HtmlBody = request.Body;
                mailmessage.Body = bodyBuilder.ToMessageBody(); ;


                using (SmtpClient smtp = new())
                {
                    smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    smtp.Connect(_emailSettings.SmtpHost,_emailSettings.SmtpPort,SecureSocketOptions.StartTls);
                    smtp.Authenticate(_emailSettings.SmtpUser,_emailSettings.SmtpPass);
                    await smtp.SendAsync(mailmessage);
                    smtp.Disconnect(true);
                }


            }
            catch (Exception ex)
            {

            }
        }
    }
}
