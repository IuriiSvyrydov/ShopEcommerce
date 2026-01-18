

using MediatR;

namespace Identity.Application.Features.Commands;

public record RefreshTokenCommand(string RefreshToken) : IRequest<RefreshTokenResult>;
public record RefreshTokenResult(string JwtToken, string RefreshToken);

