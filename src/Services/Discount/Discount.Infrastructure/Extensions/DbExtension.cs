using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace Discount.Infrastructure.Extensions;

public static class DbExtension
{
    public static IHost MigrationDatabase<TContext>(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;
        var config = services.GetRequiredService<IConfiguration>();
        var logger = services.GetRequiredService<ILogger<TContext>>();

        try
        {
            logger.LogInformation("Discount DB Migration started.");
            ApplyMigration(config, logger);
            logger.LogInformation("Discount DB Migration finished.");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Discount DB Migration failed.");
        }

        return host;
    }

    private static void ApplyMigration(IConfiguration config, ILogger logger)
    {
        var connectionString = config.GetValue<string>("DatabaseSettings:ConnectionString");
        var builder = new NpgsqlConnectionStringBuilder(connectionString);

        var retry = 5;
        while (retry > 0)
        {
            try
            {
                // 1. Подключение к системной базе postgres для создания базы, если её нет
                using (var connection = new NpgsqlConnection(
                           $"Host={builder.Host};Port={builder.Port};Username={builder.Username};Password={builder.Password};Database=postgres"))
                {
                    connection.Open();
                    using var cmd = new NpgsqlCommand($"CREATE DATABASE \"{builder.Database}\";", connection);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        logger.LogInformation($"Database {builder.Database} created.");
                    }
                    catch (PostgresException e) when (e.SqlState == "42P04") // база уже существует
                    {
                        logger.LogInformation($"Database {builder.Database} already exists.");
                    }
                }

                // 2. Подключение к нужной базе и создание таблицы + вставка данных
                using var dbConnection = new NpgsqlConnection(connectionString);
                dbConnection.Open();
                using var command = new NpgsqlCommand { Connection = dbConnection };

                command.CommandText = "DROP TABLE IF EXISTS Coupon;";
                command.ExecuteNonQuery();

                command.CommandText = @"CREATE TABLE Coupon(
                                            Id SERIAL PRIMARY KEY,
                                            ProductName VARCHAR(24) NOT NULL,
                                            Description TEXT,
                                            Amount INT
                                        );";
                command.ExecuteNonQuery();

                command.CommandText = @"INSERT INTO Coupon (ProductName, Description, Amount) 
                                        VALUES ('IPhone X', 'IPhone Discount', 150);";
                command.ExecuteNonQuery();

                break; // успешное выполнение — выходим из retry
            }
            catch (Exception e)
            {
                retry--;
                logger.LogWarning(e, $"Discount DB migration attempt failed. Retries left: {retry}");
                if (retry == 0) throw;
                Thread.Sleep(2000); // короткая пауза перед повторной попыткой
            }
        }
    }
}
