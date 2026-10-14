
using System.Text.Json;

namespace UnitConversionApi.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ArgumentException ex)
        {
            _logger.LogWarning(
                ex,
                "Invalid conversion request.");

            context.Response.StatusCode =
                StatusCodes.Status400BadRequest;

            context.Response.ContentType = "application/json";

            var response = new
            {
                status = 400,
                message = ex.Message
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "An unexpected error occurred.");

            if (context.Response.HasStarted)
            {
                throw;
            }

            context.Response.Clear();
            context.Response.StatusCode =
                StatusCodes.Status500InternalServerError;

            context.Response.ContentType = "application/json";

            var response = new
            {
                status = 500,
                message = "An unexpected error occurred."
            };

            await context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}
