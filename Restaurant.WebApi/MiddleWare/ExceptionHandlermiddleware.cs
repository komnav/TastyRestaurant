using Microsoft.AspNetCore.Http.HttpResults;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json;

namespace Restaurant.WebApi.Middleware
{
    public class ExceptionHandlermiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlermiddleware(RequestDelegate next)
        {
            _next = next;
        }


        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                await HandlerExceptionAsync(context, exception);
            }
        }

        private async Task HandlerExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var message = "An error has occurred.";

            switch (exception)
            {
                case KeyNotFoundException:
                    code = HttpStatusCode.NotFound;
                    message = JsonSerializer.Serialize("Resource not found.");
                    break;

                case ValidationException validationException:
                    code = HttpStatusCode.BadRequest;
                    message = JsonSerializer.Serialize(validationException.Message);
                    break;

                case InvalidOperationException:
                    code = HttpStatusCode.BadRequest;
                    message = JsonSerializer.Serialize(exception.Message);
                    break;

            }
            if (exception.InnerException is Npgsql.PostgresException pgException)
            {
                if (pgException.SqlState == "23505")
                {
                    code = HttpStatusCode.BadRequest;
                    message = JsonSerializer.Serialize("A record with this value already exists.");
                }
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            await context.Response.WriteAsync(message);
        }
    }
}
