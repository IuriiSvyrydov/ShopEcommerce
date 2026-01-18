

using Catalog.Infrastructure.Repositories;

namespace Infrastructure.Extensions;

public static class InfrastructureExtension
{
    public static IServiceCollection RegisterInfrastructureServices(this IServiceCollection services)
    {
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IBrandRepository, BrandRepository>();
        services.AddScoped<ITypeRepository, TypeRepository>();
        return services;
    }
}
