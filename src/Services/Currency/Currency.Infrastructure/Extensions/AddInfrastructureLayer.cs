

namespace Currency.Infrastructure.Extensions;

public static class AddInfrastructureLayer
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Register infrastructure services here
        // e.g., database contexts, repositories, external API clients, etc.
        // Example: Register a MongoDB client
        services.AddSingleton<IMongoClient>(sp =>
        {
            var connectionString = configuration.GetConnectionString("MongoDb");
            return new MongoClient(connectionString);
        });
        services.AddScoped(sp =>
        {
            var client = sp.GetRequiredService<IMongoClient>();
            var databaseName = configuration["MongoDbSettings:CurrencyDb"];
            return client.GetDatabase(databaseName);
        });
        // Register repositories
        services.AddScoped<ICurrencyRateRepository, Repositories.CurrencyRateRepository>();
        // Register external API providers
        services.AddHttpClient<ICurrencyRateProvider, ExchangeRateApiProvider>();

        return services;
    }
}
