using MediatR;

namespace Identity.Application.Features.Commands;

public record LoginUserCommand(string Email, string Password): IRequest<LoginResult>;