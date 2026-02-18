

using Microsoft.EntityFrameworkCore.Design;

namespace Ordering.Infrastructure.Data;

public sealed class OrderContextFactory : IDesignTimeDbContextFactory<OrderContext>
{
    public OrderContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<OrderContext>();
        optionsBuilder.UseSqlServer("Server=localhost,1433;Database=OrderDb;User Id=sa;Password=Passw0rd123!;TrustServerCertificate=True;Encrypt=False;",
            sql =>
            {
                sql.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null);
            });

        return new OrderContext(optionsBuilder.Options);
    }
}
