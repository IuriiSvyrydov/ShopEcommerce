
using Microsoft.EntityFrameworkCore.Design;

namespace Payment.Infrastructure.Persistence;

public sealed class PaymentFactory : IDesignTimeDbContextFactory<PaymentDbContext>
{
    public PaymentDbContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<PaymentDbContext>();
        builder.UseNpgsql("Host=localhost;Port=5432;Database=PaymentDb;Username=postgres;Password=postgres;");
        return new PaymentDbContext(builder.Options);
    }
}
