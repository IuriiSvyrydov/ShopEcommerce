

namespace Identity.Application.Features.Commands.Email.Commands;

public record SendEmailCommand(string ToEmail, string Subject, string HtmlBody) : IRequest<string>;

