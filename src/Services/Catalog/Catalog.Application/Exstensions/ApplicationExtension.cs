

using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Application.Exstensions;

public static class ApplicationExtension
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ApplicationExtension).Assembly));
        return services;
    }
}
