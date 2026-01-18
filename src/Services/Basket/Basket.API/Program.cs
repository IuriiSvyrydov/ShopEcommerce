

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterApplicationServices()
               .RegisterInfrastructureServices();
builder.Services.AddSwaggerGen(p =>
  p.SwaggerDoc("v1", new() { Title = "Basket.API", Version = "v1" }));
builder.Services.AddStackExchangeRedisCache(options =>
{
  options.Configuration = builder.Configuration.GetValue<string>("ConnectionStrings:Redis");
});


builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddScoped<IDiscountGrpcService, DiscountGrpcService>();
builder.Services.RegisterInfrastructureServices();
builder.Services.AddGrpcClient<API.Grpc.Protos.DiscountProtoService.DiscountProtoServiceClient>(o =>
{
  o.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]);
});
// Add Transient DI
builder.Services.AddMassTransit(config =>
{
  config.UsingRabbitMq((ctx, cfg) =>
  {
    cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
  });
});
builder.Services.AddCors(options =>
{
  options.AddPolicy("AllowFrontend", policy =>
  {
    policy.WithOrigins("http://localhost:4200")
          .AllowAnyHeader()
          .AllowAnyMethod()
          .AllowCredentials();
  });
});
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
builder.Host.UseSerilog(Logging.ConfigurationLogger);

var app = builder.Build();
app.UseMiddleware<CorrelationIdMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();


app.UseRouting();

app.UseCors("AllowFrontend");


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
