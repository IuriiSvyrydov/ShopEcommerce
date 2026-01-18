

using Microsoft.Extensions.DependencyInjection;

namespace Basket.Application.Extensions;

public static class RegisterExtensions
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RegisterExtensions).Assembly));
        return services;
    }
}
