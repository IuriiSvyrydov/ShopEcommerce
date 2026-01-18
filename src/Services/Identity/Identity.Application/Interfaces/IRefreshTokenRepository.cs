


using Identity.Domain.Entities;

namespace Identity.Application.Interfaces;

public interface IRefreshTokenRepository
{
    Task<RefreshToken?>GetByTokenAsync(string token);
    Task<IReadOnlyList<RefreshToken>>GetActiveTokensAsinc(Guid userId);
    Task AddAsync(RefreshToken refreshToken);
    Task UpdateAsync(RefreshToken refreshToken);
    Task RevokeAllAsync(Guid userId);
    Task SaveChangesAsync();


}
