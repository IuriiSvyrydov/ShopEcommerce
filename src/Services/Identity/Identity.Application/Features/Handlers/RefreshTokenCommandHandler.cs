using Microsoft.AspNetCore.Http;

public sealed class RefreshTokenCommandHandler
    : IRequestHandler<RefreshTokenCommand, RefreshTokenResult>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ITokenService _tokenService;
    private readonly IUserService _userService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RefreshTokenCommandHandler(
        IRefreshTokenRepository refreshTokenRepository, IUserService userService,
        ITokenService tokenService, IHttpContextAccessor httpContextAccessor)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _tokenService = tokenService;
        _userService = userService;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<RefreshTokenResult> Handle(
        RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        var refreshNewToken = _httpContextAccessor.HttpContext?.Request
            .Cookies["refreshToken"];
        if (string.IsNullOrWhiteSpace(refreshNewToken))
            throw new UnauthorizedAccessException("Refresh token is missing.");
        var token = await _refreshTokenRepository.GetByTokenAsync(refreshNewToken);

        if (token == null || token.IsExpired)
            throw new UnauthorizedAccessException("Invalid refresh token.");

        if (token.IsRevoked)
        {
            await _refreshTokenRepository.RevokeAllAsync(token.UserId);
            await _refreshTokenRepository.SaveChangesAsync();
            throw new SecurityException("Refresh token has been revoked.");
        }

        var newRefreshToken = _tokenService.GenerateRefreshToken(token.UserId);
        token.RevokedOnUtc = DateTime.UtcNow;
        token.ReplacedByTokenId = newRefreshToken.Id;
        await _refreshTokenRepository.AddAsync(newRefreshToken);
        await _refreshTokenRepository.SaveChangesAsync();
        var user = await _userService.GetUserByIdAsync(token.UserId);
        var jwtToken = _tokenService.GenerateJwtToken(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email,
            user.Roles);
        return new RefreshTokenResult(jwtToken, newRefreshToken.Token);


    }
}