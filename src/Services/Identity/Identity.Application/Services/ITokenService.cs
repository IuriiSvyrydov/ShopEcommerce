
using Identity.Domain.Entities;

namespace Identity.Application.Services;

public interface ITokenService
{
    string GenerateJwtToken(
     Guid userId,
     string email,
     string firstName,
     string lastName,
     IEnumerable<string> roles);
    RefreshToken GenerateRefreshToken(Guid userId);
    string HashRefreshToken(string token);

}