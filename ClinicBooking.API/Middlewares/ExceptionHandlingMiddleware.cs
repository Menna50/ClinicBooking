
using ClinicBooking.Shared.Results;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace ClinicBooking.API.Middlewares
{
    public class ExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
        catch (Exception ex)
            {
            await    HandleExceptionAsync(context, ex);
            }

        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = StatusCodes.Status500InternalServerError;

            await context.Response.WriteAsync(JsonSerializer.Serialize(new Error("SERVER.UNEXPECTED_ERROR",
                "An unexpected error occurred. Please try again later." )));

        }
    }




   public static class ExceptionHandlingMiddlewareExtension
    {
        public static IApplicationBuilder UseExceptionHandlinMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlingMiddleware>();
            return app; 
        }
       
    }
}
