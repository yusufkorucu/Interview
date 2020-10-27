using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Interview.Business.Abstract;
using Interview.Entities.Concrete;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Hosting;
using MimeKit;
using MailKit.Net.Smtp;

namespace Interview.Business.Concrete
{
    public class MailerManager : IMailer
    {
        private readonly SmtpSettings _smtpSettings;

        public MailerManager(IOptions<SmtpSettings> smtpSetting)
        {
            _smtpSettings = smtpSetting.Value;
        }

        [Obsolete]
        public async Task SendEmailAsync(string email, string subject, string body)
        {
            try
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.SenderEmail));
                message.To.Add(new MailboxAddress(email));
                message.Subject = subject;
                message.Body = new TextPart("html")
                {
                    Text = body
                };

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {
                    client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    await client.ConnectAsync(_smtpSettings.Server);
                    await client.AuthenticateAsync(_smtpSettings.UserName, _smtpSettings.Password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }
            catch (Exception e)
            {
                throw new InvalidOperationException(e.Message);
            }
        }
    }
}

