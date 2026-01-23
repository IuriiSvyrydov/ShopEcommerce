public sealed class ForgotPasswordCommandHandler
    : IRequestHandler<ForgotPasswordCommand, Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public ForgotPasswordCommandHandler(
        IUserRepository userRepository,
        IEmailService emailService)
    {
        _userRepository = userRepository;
        _emailService = emailService;
    }

    
    public async Task<Unit> Handle(
    ForgotPasswordCommand request,
    CancellationToken cancellationToken)
    {
        // Получаем пользователя по email или телефону
        var user = await _userRepository.GetByEmailAsync(request.EmailOrPhone);
        if (user == null)
            return Unit.Value; // если пользователь не найден — ничего не делаем

        // Генерируем токен для сброса пароля
        var token = await _userRepository.GeneratePasswordResetTokenAsync(user.Id);
        if (token == null)
            return Unit.Value; // если токен не сгенерирован — ничего не делаем

        // Формируем ссылку для сброса пароля
        var resetLink = $"{request.ClientUrl.TrimEnd('/')}/auth/reset-password" +
                        $"?userId={user.Id}&token={Uri.EscapeDataString(token)}";

        // Создаем HTML-содержимое письма
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
             Reset password
          </a>
        </p>
        <p>This link is valid for a limited time.</p>";

        // Отправляем письмо пользователю
        await _emailService.SendEmailAsync(
            user.Email,
            "Reset your password",
            htmlBody);

        return Unit.Value;
    }

}
