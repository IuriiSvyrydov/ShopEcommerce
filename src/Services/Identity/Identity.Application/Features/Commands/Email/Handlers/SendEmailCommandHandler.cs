

using Identity.Application.Features.Commands.Email.Commands;

namespace Identity.Application.Features.Commands.Email.Handlers;

public sealed class SendEmailCommandHandler : IRequestHandler<SendEmailCommand, string>
{
    private readonly IEmailService _emailService;
    public SendEmailCommandHandler(IEmailService emailService)
    {
        _emailService = emailService;
    }
    public async Task<string> Handle(SendEmailCommand request, CancellationToken cancellationToken)
    {
        var result = await _emailService.SendEmailAsync(request.ToEmail, request.Subject, request.HtmlBody);
        return result;

    }
}
