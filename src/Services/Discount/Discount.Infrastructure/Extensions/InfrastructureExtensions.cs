

using Discount.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Discount.Domain.Repositories;

namespace Discount.Infrastructure.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection RegisterDiscountInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IDiscountRepository, DiscountRepository>();
        return services;
    }
}
