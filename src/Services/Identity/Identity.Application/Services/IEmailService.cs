

namespace Identity.Application.Services;

public interface IEmailService
{
    Task<string> SendEmailAsync(string toEmail, string subject, string htmlBody);
}
