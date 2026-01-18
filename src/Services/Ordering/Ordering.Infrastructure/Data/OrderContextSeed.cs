
using Microsoft.Extensions.Logging;

namespace Ordering.Infrastructure.Data;

public class OrderContextSeed
{
    public static async Task SeedAsync(OrderContext orderContext, ILogger<OrderContextSeed> logger)
    {
        if (!orderContext.Orders.Any())
        {
            orderContext.Orders.AddRange(GetPreconfiguredOrders());
            await orderContext.SaveChangesAsync();
            logger.LogInformation("Seeded database with preconfigured orders.");
        }
    }

    private static IEnumerable<Order> GetPreconfiguredOrders()
    {
        return new List<Order>
        {
            new Order
            {
                UserName = "testuser",
                FirstName = "Gerro5",
                LastName = "User",
                EmailAddress = "test@test.com",
                AddressLine = "123 Main St",
                Country = "USA",
                TotalPrice = 350,
                State = "NY",
                ZipCode = "10001",
                CardName = "Test User",
                CardNumber = "1234567890123456",
                CreatedBy = "system",
                Expiration = "12/25",
                CVV = "123",
                PaymentMethod = 1,
              


            }
    };
        }
}
