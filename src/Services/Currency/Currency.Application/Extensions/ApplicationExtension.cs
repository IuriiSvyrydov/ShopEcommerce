

namespace Currency.Application.Extensions;

public static class ApplicationExtension
{
    public static IServiceCollection RegisterApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<ICurrencyService, CurrencyService>();
     //   services.AddScoped<ICurrencyRateRepository, CurrencyRateRepository>();

        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        return services;
    }
}
