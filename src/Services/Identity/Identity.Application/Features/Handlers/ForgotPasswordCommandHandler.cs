

namespace Identity.Application.Features.Handlers;

public sealed class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand,Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;
    public ForgotPasswordCommandHandler(IUserRepository userRepository, IEmailService emailService)
    {
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task<Unit> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        var user  = await _userRepository.GetByEmailAsync(request.EmailOrPhone);
        if (user == null) return Unit.Value;
        var resetToken = await _userRepository.GeneratePasswordResetTokenAsync(user.Id);
        if(resetToken==null) return Unit.Value;
        var resetLink = $"{request.ClientUrl}/reset-password" +
                                    $"?userId={user.Id}&token={Uri.EscapeDataString(resetToken)}";
        var htmlBody = $@"
                <h2>Reset Password</h2>
                <p>You have requested a password reset.</p>
                <p>
                <a href='{resetLink}'
                   style='padding:10px 15px;
                          background:#4f46e5;
                          color:white;
                          text-decoration:none;
                          border-radius:5px;'>
                   Сбросить пароль
                </a>
            </p>
            <p>Ссылка действительна ограниченное время.</p>";
        await _emailService.SendEmailAsync(user.Email, "Reset Password", htmlBody);



        return Unit.Value;

    }
}
