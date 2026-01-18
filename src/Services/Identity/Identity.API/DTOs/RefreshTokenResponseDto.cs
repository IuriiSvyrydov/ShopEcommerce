namespace Identity.API.DTOs;

public record RefreshTokenResponseDto(string JwtToken,
    string RefreshToken);

