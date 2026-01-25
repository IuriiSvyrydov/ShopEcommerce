using Currency.Application.Extensions;
using Currency.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddControllers();

builder.Services.AddOpenApi();
builder.Services.RegisterApplicationLayer();
builder.Services.RegisterInfrastructureLayer(builder.Configuration);

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Currency.API", Version = "v1" });
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseSwagger();
app.UseSwaggerUI(c => c.RoutePrefix = "swagger");
app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
