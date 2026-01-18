
namespace Identity.Application.Features.Commands;

public record ResetPasswordCommand(Guid UserId, string Token, string NewPassword) : IRequest<Unit>;

