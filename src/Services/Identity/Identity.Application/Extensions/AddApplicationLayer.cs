using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Application.Extensions;

public static class AddApplicationLayer
{
    public static IServiceCollection RegisterApplicationLayer(this IServiceCollection services)
    {
        services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        return services;
    }
}