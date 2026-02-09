

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Payment.Application.Extensions;

public static class AddApplicationExtension
{
    public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return services;
    }
}
