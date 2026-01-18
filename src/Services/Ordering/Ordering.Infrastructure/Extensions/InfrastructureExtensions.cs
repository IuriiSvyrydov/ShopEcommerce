

namespace Ordering.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add DbContext
        services.AddDbContext<OrderContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("OrderConnectionString"), sqlopt =>
            {
                sqlopt.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(10),
                    errorNumbersToAdd: null);
            }));
        // Add Repositories
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
        return services;
    }
}
