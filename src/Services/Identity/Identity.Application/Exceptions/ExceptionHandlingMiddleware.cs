using Identity.Application.Exceptions;
using Microsoft.AspNetCore.Http;
using System.Net;

using System.Text.Json;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (IdentityException ex)
        {
            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.Response.ContentType = "application/json";

            var response = new
            {
                message = ex.Message,
                errors = ex.Errors
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}
