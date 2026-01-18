using Serilog.Context;

namespace ApiGateway.Middleware;

public class CorrelationIdMiddleware
{
  private const string CorrelationIdHeader = "x-correlation-Id";
  private  readonly RequestDelegate _next;
  private readonly ILogger<CorrelationIdMiddleware> _logger;
  public CorrelationIdMiddleware(RequestDelegate next, ILogger<CorrelationIdMiddleware> logger)
  {
    _next = next;
    _logger = logger;
  }
  public async Task InvokeAsync(HttpContext context)
  {
    if (!context.Request.Headers.TryGetValue(CorrelationIdHeader, out var correlationId))
    {
      correlationId = Guid.NewGuid().ToString();
      context.Request.Headers[CorrelationIdHeader] = correlationId;
      
    }
    context.Response.Headers[CorrelationIdHeader] = correlationId;
    using (LogContext.PushProperty("CorrelationId", correlationId))
    {
      _logger.LogInformation("Starting request {CorrelationId}", correlationId);
      await _next(context);
    }
  }
    
}