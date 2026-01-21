

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // ===================== SERVICES =====================
        builder.Services.AddControllers()
            .AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });
        builder.Services.AddOpenApi();
        builder.Services.AddSwaggerGen(p =>
          p.SwaggerDoc("v1", new() { Title = "Identity.API", Version = "v1" }));

        builder.Services.RegisterInfrastructureLayer(builder.Configuration);
        builder.Services.RegisterIdentity(builder.Configuration);
        builder.Services.RegisterApplicationLayer();

        builder.Host.UseSerilog(Logging.ConfigurationLogger);

        // ===================== APP =====================
        var app = builder.Build();
        using (var scope = app.Services.CreateScope())
        {
            var db = scope.ServiceProvider
                .GetRequiredService<ApplicationUserDbContext>();

            db.Database.Migrate();
        }


        // ===================== MIGRATIONS =====================

        // ===================== PIPELINE =====================
        app.UseSwagger();
        app.UseSwaggerUI();
       

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }
}