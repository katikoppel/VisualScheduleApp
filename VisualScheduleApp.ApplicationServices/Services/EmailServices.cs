using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using MimeKit;
using VisualScheduleApp.Models.Email;

namespace VisualScheduleApp.Services
{
    public class EmailServices: IEmailSender
    {
        private readonly EmailSettings _emailSettings;

        public EmailServices(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var message = new MimeMessage();

            message.From.Add(new MailboxAddress(
                _emailSettings.SenderName,
                _emailSettings.SenderEmail));

            message.To.Add(MailboxAddress.Parse(email));
            message.Subject = subject;

            var builder = new BodyBuilder
            {
                HtmlBody = htmlMessage
            };

            message.Body = builder.ToMessageBody();

            using var client = new SmtpClient();

            await client.ConnectAsync(
                _emailSettings.SmtpServer,
                _emailSettings.Port,
                SecureSocketOptions.StartTls);

            await client.AuthenticateAsync(
                _emailSettings.UserName,
                _emailSettings.Password);

            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }
    }
}