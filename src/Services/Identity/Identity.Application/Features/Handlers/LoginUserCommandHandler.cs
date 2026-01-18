using Identity.Application.Features.Commands;
using Identity.Application.Services;
using MediatR;

namespace Identity.Application.Features.Handlers;

public class LoginUserCommandHandler: IRequestHandler<LoginUserCommand, LoginResult>
{
    private readonly IIdentityService _identityService;

    public LoginUserCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<LoginResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        return await _identityService.LoginAsync(request.Email, request.Password);
    }
}