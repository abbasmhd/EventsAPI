using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace EventsAPI.Helpers {

    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errorsInModelState = context.ModelState
                                                .Where(m => m.Value.Errors.Any())
                                                .ToDictionary(k => k.Key, v => v.Value.Errors.Select(x => x.ErrorMessage))
                                                .ToArray();

                var errorResponse = new ErrorResponse();

                foreach (var (key, value) in errorsInModelState)
                {
                    foreach (var error in value)
                    {
                        errorResponse.Errors.Add(new ErrorModel()
                        {
                            Field = key,
                            Message = error
                        });
                    }
                }

                context.Result = new BadRequestObjectResult(errorResponse);
                return;
            }

            await next();
        }
    }
}
