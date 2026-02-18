

using ApiGateway.Middleware;
using Common.Logging;
using Ordering.Infrastructure.Dispatcher;
using Serilog;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.AddInfrastructureServices(builder.Configuration)
    .RegisterApplicationServices();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Ordering.API", Version = "v1" });
});
builder.Services.AddMassTransit(config =>
{
    config.AddConsumer<BasketOrderingConsumer>();
    config.AddConsumer<PaymentCompletedConsumer>();
    config.AddConsumer<PaymentFailedConsumer>();
    config.UsingRabbitMq((ctx, cfg) =>
    {

        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        cfg.ReceiveEndpoint(EventBusConstant.BasketCheckoutQueue, e =>
        {
            e.ConfigureConsumer<BasketOrderingConsumer>(ctx);
        });
        //Payment completed.
        cfg.ReceiveEndpoint(EventBusConstant.PaymentCompletedQueue, c =>
        {
            c.ConfigureConsumer<PaymentCompletedConsumer>(ctx);
        });
        //payment failed
        cfg.ReceiveEndpoint(EventBusConstant.PaymentFailedQueue, c =>
        {
            c.ConfigureConsumer<PaymentFailedConsumer>(ctx);
        });
    });
});
builder.Services.AddHostedService<OutboxMessageDispatcher>();
builder.Host.UseSerilog(Logging.ConfigurationLogger);


var app = builder.Build();
app.UseMiddleware<CorrelationIdMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MigrateDatabaseAsync<OrderContext>((context, services) =>
{
    var logger = services.GetService<ILogger<OrderContextSeed>>();
    OrderContextSeed.SeedAsync(context, logger).Wait();
});
app.UseSwagger();
app.UseSwaggerUI(c =>c.SwaggerEndpoint("/swagger/v1/swagger.json", "Ordering.API v1"));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();