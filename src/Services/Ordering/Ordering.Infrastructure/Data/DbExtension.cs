

using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Polly;
using System.Collections;

namespace Ordering.Infrastructure.Data;

public static class DbExtension
{
    public static IHost MigrateDatabaseAsync<TContext>(
     this IHost host,
     Action<TContext, IServiceProvider> seeder)
     where TContext : DbContext
    {
        using (var scope = host.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var context = services.GetService<TContext>();
            var logger = services.GetRequiredService<ILogger<TContext>>();
            try
            {
                logger.LogInformation("Migrating database associated with context {DbContextName}", typeof(TContext).Name);
                var retry = Policy.Handle<SqlException>()
                    .WaitAndRetry(
                       retryCount: 5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                       onRetry: (exception, timeSpan, retryCount, context) =>
                        {
                            logger.LogWarning(exception, "Exception {ExceptionType} with message {Message} detected on attempt {RetryCount} of database migration for context {DbContextName}",
                                exception.GetType().Name, exception.Message, retryCount, typeof(TContext).Name);
                        });
                retry.Execute(() => CallSeeder(seeder, context, services));
                

                logger.LogInformation("Migrated database associated with context {DbContextName}", typeof(TContext).Name);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while migrating the database used on context {DbContextName}", typeof(TContext).Name);
                throw;
            }
        }
        return host;
    }

  

    private static void CallSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext context, IServiceProvider services) where TContext : DbContext
    {
        context.Database.Migrate();
        seeder(context, services);
    }
}
