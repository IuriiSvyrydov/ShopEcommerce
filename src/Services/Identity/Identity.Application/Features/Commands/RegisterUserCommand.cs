

using MediatR;

namespace Identity.Application.Features.Commands;

public record RegisterUserCommand(string FirstName,
    string LastName, string Email, string Password) : IRequest;

