
namespace Identity.Application.Features.Commands;

public record ForgotPasswordCommand(string EmailOrPhone, string ClientUrl) : IRequest<Unit>;

