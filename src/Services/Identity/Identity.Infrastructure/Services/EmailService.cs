

using Identity.Application.Settings.Email;
using Microsoft.Extensions.Options;

namespace Identity.Infrastructure.Services
{
    public sealed class EmailService : IEmailService
    {
        private EmailSettings _emailSettings;
        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            _emailSettings = emailSettings.Value;
        }
        public async Task<string> SendEmailAsync(string toEmail, string subject, string htmlBody)
        {
           var message = new MimeKit.MimeMessage();
              message.From.Add(new MimeKit.MailboxAddress(_emailSettings.FromName, _emailSettings.FromEmail));
                message.To.Add(MimeKit.MailboxAddress.Parse(toEmail));
                message.Subject = subject;
                message.Body = new MimeKit.BodyBuilder { HtmlBody = htmlBody }.ToMessageBody();
                using var client = new MailKit.Net.Smtp.SmtpClient();
            try
            {
                await client.ConnectAsync(
                    _emailSettings.SmtpHost,
                    _emailSettings.SmtpPort,
                    _emailSettings.UseSsl?
                    MailKit.Security.SecureSocketOptions.StartTls : MailKit.Security.SecureSocketOptions.None

                ); 
                await client.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
                await client.SendAsync(message);
            }
            finally
            {
                await client.DisconnectAsync(true);
            }
            return "Email sent successfully";

        }
    }
}
