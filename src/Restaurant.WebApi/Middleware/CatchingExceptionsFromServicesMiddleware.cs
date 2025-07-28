using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json;

namespace Restaurant.WebApi.Middleware;

internal sealed class CatchingExceptionsFromServicesMiddleware
{
    private readonly ILogger<CatchingExceptionsFromServicesMiddleware> _logger;
    private readonly RequestDelegate _next;

    public CatchingExceptionsFromServicesMiddleware(
        ILogger<CatchingExceptionsFromServicesMiddleware> logger,
        RequestDelegate next)
    {
        _logger = logger;
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);

            await HandleExceptionAsync(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = GetStatusCode(exception);

        var response = new
        {
            status = statusCode,
            detail = exception.Message,
            errors = GetErrors(exception)
        };

        httpContext.Response.ContentType = "application/json";

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            BadHttpRequestException => StatusCodes.Status400BadRequest,
            KeyNotFoundException => StatusCodes.Status404NotFound,
            ValidationException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };

    static string GetErrors(Exception? exception)
    {
        StringBuilder errorBuilder = new StringBuilder();

        while (exception != null)
        {
            errorBuilder.AppendLine(exception.Message);
            exception = exception.InnerException;
        }

        return errorBuilder.ToString();
    }
}