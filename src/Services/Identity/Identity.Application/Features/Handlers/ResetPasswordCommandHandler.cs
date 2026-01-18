


using Identity.Application.Exceptions;

namespace Identity.Application.Features.Handlers;

public sealed class ResetPasswordCommandHandler : IRequestHandler<ResetPasswordCommand, Unit>
{
    private readonly IUserRepository _userRepository;
    public ResetPasswordCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    public async Task<Unit> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
       var success = await _userRepository.ResetPasswordAsync(request.UserId, request.Token, request.NewPassword );
        if(!success)
         
          throw new InvalidOperationException("Failed to reset password.");
        return Unit.Value;

    }
}
