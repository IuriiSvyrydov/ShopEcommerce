using Payment.Application.Extensions;
using Payment.Infrastructure.Consumers;
using MassTransit;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// =====================
// Configuration
// =====================
var configuration = builder.Configuration;

// =====================
// Services
// =====================
builder.Services.AddControllers();

// Swagger / OpenAPI
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Payment.API", Version = "v1" });
});
builder.Services.AddOpenApi();

// Serilog
builder.Host.UseSerilog(Logging.ConfigurationLogger);

// MassTransit + RabbitMQ
builder.Services.AddMassTransit(cfg =>
{
    cfg.SetKebabCaseEndpointNameFormatter();

    cfg.UsingRabbitMq((context, busCfg) =>
    {
        var host = builder.Configuration["EventBusSettings:HostAddress"];

        if (string.IsNullOrWhiteSpace(host))
            throw new InvalidOperationException("EventBusSettings:HostAddress is missing");

        busCfg.Host(new Uri(host));
    });
});


// Application & Infrastructure services
builder.Services.RegisterApplicationServices()
       .RegisterInfrastructureServices(configuration);

// =====================
// Build app
// =====================
var app = builder.Build();

// Middleware
app.UseMiddleware<CorrelationIdMiddleware>();

// Swagger only in Development

    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payment.API v1");

    });

    // HTTPS & Authorization
    app.UseHttpsRedirection();
    app.UseAuthorization();

    // Controllers
    app.MapControllers();

    // Run
    app.Run();

