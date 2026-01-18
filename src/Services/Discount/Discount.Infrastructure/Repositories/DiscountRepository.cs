using Dapper;
using Discount.Domain.Entities;
using Discount.Domain.Repositories;

using Microsoft.Extensions.Configuration;


namespace Discount.Infrastructure.Repositories;

public sealed class DiscountRepository : IDiscountRepository
{
    private readonly string? _connectionString;

    public DiscountRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetValue<string>("DatabaseSettings:ConnectionString")!;

    }

    public async Task<bool> CreateDiscountAsync(Coupon coupon)
    {
        await using var connection = new Npgsql.NpgsqlConnection(_connectionString);
        var affected =
            await connection.ExecuteAsync(
                "INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
                new { coupon.ProductName, coupon.Description, coupon.Amount });
        return affected >0;
    }

    public async Task<bool> DeleteDiscountAsync(string productName)
    {
        
            await using var connection = new Npgsql.NpgsqlConnection(_connectionString);
            var affected =
                await connection.ExecuteAsync(
                    "DELETE FROM Coupon WHERE ProductName = @ProductName",
                    new { ProductName = productName });
        return affected >0;

    }

    public async Task<Coupon> GetDiscountAsync(string productName)
    {
        await using var connection = new Npgsql.NpgsqlConnection(_connectionString);
        var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>(
            "SELECT * FROM Coupon WHERE ProductName = @ProductName",
            new { ProductName = productName });
        if (coupon == null)
            {
            return new Coupon
            {
                ProductName = "No Discount",
                Description = "No Discount Description",
                Amount = 0
            };
        }
        return coupon;
    }

    public async Task<bool> UpdateDiscountAsync(Coupon coupon)
    {
        await using var connection = new Npgsql.NpgsqlConnection(_connectionString);
        var affected =
            await connection.ExecuteAsync(
                "UPDATE Coupon SET ProductName = @ProductName, Description = @Description, Amount = @Amount WHERE Id = @Id",
                new { coupon.ProductName, coupon.Description, coupon.Amount, coupon.Id });
        return affected > 0;
    }
}
