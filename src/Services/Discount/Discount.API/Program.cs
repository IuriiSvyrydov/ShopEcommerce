using Common.Logging;
using Discount.API.Services;
using Discount.Application.Features.Handlers;
using Discount.Infrastructure.Extensions;
using Serilog;
using System.Reflection;
using ApiGateway.Middleware;

var builder = WebApplication.CreateBuilder(args);


var assemblies = new Assembly[]
{
            Assembly.GetExecutingAssembly(),
            typeof(CreateDiscountCommandHandler).Assembly
};
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(assemblies);
});
builder.Services.RegisterDiscountInfrastructureServices();
builder.Services.AddGrpc();
builder.Host.UseSerilog(Logging.ConfigurationLogger);


//builder.Services.AddControllers();

builder.Services.AddOpenApi();

var app = builder.Build();
app.UseMiddleware<CorrelationIdMiddleware>();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

//app.UseHttpsRedirection();

//app.UseAuthorization();

//app.MapControllers();
app.MigrationDatabase<Program>();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<DiscountService>();
});


app.Run();
