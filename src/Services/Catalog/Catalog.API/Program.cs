

using ApiGateway.Middleware;
using Catalog.Application.Exstensions;

namespace Catalog.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);



        builder.Services.AddControllers();

        builder.Services.AddOpenApi();
        builder.Services.RegisterApplicationServices();
        builder.Services.RegisterInfrastructureServices();
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
        BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));
        builder.Services.AddSwaggerGen(c =>
        {

            c.SwaggerDoc("v1", new() { Title = "Catalog.API", Version = "v1" });
   
        });
        //var env = builder.Environment;
        //DatabaseSeeder.SeedAsync(builder.Configuration).Wait();


     
        var app = builder.Build();
        app.UseMiddleware<CorrelationIdMiddleware>();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
        }
        app.UseSwagger();
        app.UseSwaggerUI(c=>c.RoutePrefix = "swagger");
       
        using (var scope = app.Services.CreateScope())
        {
            var config = scope.ServiceProvider.GetRequiredService<IConfiguration>();
            await DatabaseSeeder.SeedAsync(config);
        }
        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
