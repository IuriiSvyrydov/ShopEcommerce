using ApiGateway.Middleware;
using Common.Logging;
using Identity.API.Extensions;
using Identity.Application.Extensions;
using Identity.Infrastructure.Data;
using Identity.Infrastructure.Extensions;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// ===================== SERVICES =====================
builder.Services.AddControllers()
    .AddJsonOptions(o =>
    {
        o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    });
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.RegisterInfrastructureLayer(builder.Configuration);
builder.Services.RegisterIdentity(builder.Configuration);
builder.Services.RegisterApplicationLayer();

builder.Host.UseSerilog(Logging.ConfigurationLogger);

// ===================== APP =====================
var app = builder.Build();

// ===================== MIGRATIONS =====================

// ===================== PIPELINE =====================
app.UseSwagger();
app.UseSwaggerUI();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
