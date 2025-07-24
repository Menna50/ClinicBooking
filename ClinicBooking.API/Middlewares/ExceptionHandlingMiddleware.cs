
using ClinicBooking.Shared.Results;
using FluentValidation;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace ClinicBooking.API.Middlewares
{
    public class ExceptionHandlingMiddleware 
    {
         RequestDelegate _next;
        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;   
        }
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
        catch (Exception ex)
            {
            await    HandleExceptionAsync(context, ex);
            }

        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = StatusCodes.Status500InternalServerError;
            var errorResponse = new Error("SERVER.UNEXPECTED_ERROR",
               exception.Message+ "An unexpected error occurred. Please try again later.");

            switch (exception)
            {
                case ValidationException validationException:
                    statusCode = StatusCodes.Status400BadRequest;

                    errorResponse = new Error("VALIDATION.ERROR",
                        string.Join("; ", validationException.Errors.Select(e => e.ErrorMessage)));
                    break;

                case KeyNotFoundException:
                    statusCode = StatusCodes.Status404NotFound;
                    errorResponse = new Error("NOT_FOUND", "The requested resource was not found.");
                    break;

                case UnauthorizedAccessException:
                    statusCode = StatusCodes.Status401Unauthorized;
                    errorResponse = new Error("UNAUTHORIZED", "You are not authorized.");
                    break;

              
               
                    //case ForbiddenAccessException:
                    //    statusCode = StatusCodes.Status403Forbidden;
                    //    errorResponse = new Error("FORBIDDEN", "You don't have permission to access this resource.");
                    //    break;

                    // مفيش default هنا، لأنه متغطي بالفعل بالقيم المبدئية
            }

            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
    }


   public static class ExceptionHandlingMiddlewareExtension
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            return app; 
        }
       
    }
}
