
namespace Payment.Infrastructure.Persistence
{
    public sealed class PaymentDbContext: DbContext
    {
        public PaymentDbContext(DbContextOptions<PaymentDbContext> options) : base(options)
        {
        }
        public DbSet<Domain.Entities.Payment> Payments => Set<Domain.Entities.Payment>();



    }
}
