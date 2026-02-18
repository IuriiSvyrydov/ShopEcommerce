using Identity.Application.Interfaces;
using Identity.Infrastructure.Mapping;

namespace Identity.Infrastructure.Interfaces;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly ApplicationUserDbContext _context;
    public RefreshTokenRepository(ApplicationUserDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(RefreshToken refreshToken)
    {
        // Добавляем именно в DbSet сущностей инфраструктуры
        _context.Tokens.Add(refreshToken.ToEntity());
        await _context.SaveChangesAsync();
    }

    public async Task<IReadOnlyList<RefreshToken>> GetActiveTokensAsinc(Guid userId)
    {
        return await _context.Tokens
           .AsNoTracking()
           .Where(rt => rt.UserId == userId && rt.RevokedOnUtc == null && rt.ExpiresOnUtc > DateTime.UtcNow)
           .Select(rt => rt.ToDomain())
           .ToListAsync();

    }

    public async Task<RefreshToken?> GetByTokenAsync(string token)
    {
        var entity = await _context.Tokens
            .AsNoTracking()
            .FirstOrDefaultAsync(rt => rt.Token == token);

        return entity?.ToDomain();
    }

    public async Task RevokeAllAsync(Guid userId)
    {
        var tokens = await _context.Tokens
            .Where(rt => rt.UserId == userId && rt.RevokedOnUtc == null)
            .ToListAsync();
        foreach (var token in tokens)
        {
            token.RevokedOnUtc = DateTime.UtcNow;
            // Optionally, you can log the reason for revocation somewhere
        }
        await _context.SaveChangesAsync();

    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();

        
    }

    public async Task UpdateAsync(RefreshToken refreshToken)
    {
        _context.Tokens.Update(refreshToken.ToEntity());
        await _context.SaveChangesAsync();
    }
}
