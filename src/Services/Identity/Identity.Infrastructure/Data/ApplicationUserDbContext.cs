

using Identity.Domain.Entities;
using Identity.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace Identity.Infrastructure.Data;

public class ApplicationUserDbContext: IdentityDbContext<ApplicationUser,IdentityRole<Guid>,Guid>
{
    public ApplicationUserDbContext(DbContextOptions<ApplicationUserDbContext> options): base(options) { }
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<RefreshTokenEntity> Tokens=> Set<RefreshTokenEntity>();
    override protected void OnModelCreating(ModelBuilder builder)
    {
      
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(builder);
    }
}

