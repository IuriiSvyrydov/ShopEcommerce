
namespace Identity.Application.Settings.Email;

public class SmsSettings
{
    public string AccountSid { get; set; } = null!;
    public string  AuthToken { get; set; } = null!;
    public string FromNumber { get; set; } = null!;
}
