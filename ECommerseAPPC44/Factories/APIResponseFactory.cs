using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Shared.ErrorModels;

namespace ECommerseAPPC04.Factories
{
    public class APIResponseFactory
    {
        public static IActionResult GenerateApiValidationErrorResponse(ActionContext Context)
        {
                var Errors = Context.ModelState.Where(M => M.Value.Errors.Any())
                .Select(M => new ValidationErros()
                {
                    Filed = M.Key,
                    Errors = M.Value.Errors.Select(E => E.ErrorMessage)

                });
            var Response = new ValidationErrorToReturn()
            {
                ValidationErros = Errors,
            };
                    return new BadRequestObjectResult(Response);
            
        }
    }
}
