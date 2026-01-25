namespace Currency.Infrastructure.Extensions;

public static class AddInfrastructureLayer
{
    public static IServiceCollection RegisterInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
       
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
