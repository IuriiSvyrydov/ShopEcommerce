

using Identity.Application.Features.Commands;
using Identity.Application.Services;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Identity.Application.Features.Handlers;

public sealed class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
{
    private readonly IIdentityService _identityService;
    private readonly ILogger<RegisterUserCommandHandler> _logger;

    public RegisterUserCommandHandler(IIdentityService identityService, ILogger<RegisterUserCommandHandler> logger)
    {
        _identityService = identityService;
        _logger = logger;
    }

    public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        await _identityService.RegisterAsync(request.FirstName, request.LastName, request.Email, request.Password);
        _logger.LogInformation("User {Email} registered successfully",request.Email);
    }
}
