

using FluentValidation;

namespace WebApi.Middleware
{
    public sealed class ExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex, "Validation error occurred");
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                var errors = ex.Errors.Select(e => new
                {
                    e.PropertyName,
                    e.ErrorMessage
                });

                await context.Response.WriteAsJsonAsync(new { errors });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred");
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new { error = "An unexpected error occurred" });
            }
        }
    }
}
