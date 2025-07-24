using Npgsql;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Application.Dtos;
using Application.Exceptions;


namespace Restaurant.WebApi.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(
            RequestDelegate next,
            IWebHostEnvironment environment,
            ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _environment = environment;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                var service = context.RequestServices.GetService<IAuthenticationService>();


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
            var message = _environment.IsProduction() ? "Internal server error" : GetMessageDetails(exception);

            if (IsPostgresDuplicateException(exception) || exception is ResourceAlreadyExistException)
            {
                code = HttpStatusCode.Conflict;
                message = "Duplicate data was added.";
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            var errorResponse = new ErrorResponseModel((int)code, message);
            await context.Response.WriteAsJsonAsync(errorResponse);

            if (code == HttpStatusCode.InternalServerError)
            {
                _logger.LogError(exception, message);
            }
        }

        bool IsPostgresDuplicateException(Exception? exception)
        {
            while (exception != null)
            {
                if (exception is PostgresException postgresException &&
                    postgresException.SqlState == PostgresErrorCodes.UniqueViolation)
                {
                    return true;
                }

                exception = exception.InnerException;
            }

            return false;
        }

        string GetMessageDetails(Exception? exception)
        {
            StringBuilder messageBuilder = new StringBuilder();

            while (exception != null)
            {
                messageBuilder.AppendLine(exception.Message);
                exception = exception.InnerException;
            }

            return messageBuilder.ToString();
        }
    }
}