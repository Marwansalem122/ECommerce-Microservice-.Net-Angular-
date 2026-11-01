

namespace eCommerce.API.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext httpContext)
        {
            try {
                await _next(httpContext);

            }
            catch(Exception ex)
            {
                _logger.LogError($"{ex.GetType()}: {ex.Message}");
                if(ex.InnerException != null)
                {
                    _logger.LogError($"Inner Exception: {ex.InnerException.GetType()}: {ex.InnerException.Message}");
                }
                // Handle the exception and return a custom response
                httpContext.Response.StatusCode = 500; // Internal Server Error

                httpContext.Response.ContentType = "application/json";
                var errorResponse = new {
                    message = ex.Message,
                    Type = ex.GetType().ToString() 
                };
                await httpContext.Response.WriteAsJsonAsync(errorResponse);
            } 

           
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlingMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
