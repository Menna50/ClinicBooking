
using Microsoft.AspNetCore.Mvc.Filters; 
using Microsoft.AspNetCore.Mvc; 
using FluentValidation; 
using FluentValidation.Results;
using System.Linq;
using System.Threading.Tasks;
using ClinicBooking.Shared.Results;

namespace ClinicBooking.API.Filters
{
    public class CustomFluentValidationFilter : IAsyncActionFilter
    {
        private readonly IServiceProvider _serviceProvider; 

        public CustomFluentValidationFilter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
          
            if (context.ActionArguments.Count == 0)
            {
                await next(); 
                return;
            }

            foreach (var argument in context.ActionArguments.Values)
            {
                if (argument == null) continue; // Skip null arguments

                var validatorType = typeof(IValidator<>).MakeGenericType(argument.GetType());
                var validator = _serviceProvider.GetService(validatorType) as IValidator;

                if (validator != null)
                {
                    var validationContext = new ValidationContext<object>(argument);

                    var validationResult = await validator.ValidateAsync(validationContext, context.HttpContext.RequestAborted);

                    if (!validationResult.IsValid)
                    {
                        var errors = validationResult.Errors
                            .Select(e => e.ErrorMessage)
                            .ToList();

                      

                        var combinedErrorMessage = string.Join(" ", errors);
                        var customErrorResponse = new Error(combinedErrorMessage, "Welcom from Validation Global Filter");

                        context.Result = new BadRequestObjectResult(customErrorResponse);
                        return; // Stop further execution of the action
                    }
                }
            }

            await next();
        }
    }
}
