using curso.api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace curso.api.Filters
{
    public class CustomModelStateValidation : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var ModelState = context.ModelState;
            if (!ModelState.IsValid)
            {

                var fieldValidatorViewModel = new FieldValidatorViewModelOutput(
                    ModelState.SelectMany(selected => selected.Value.Errors)
                        .Select(s => s.ErrorMessage)
                );
                context.Result = new BadRequestObjectResult(fieldValidatorViewModel);
            }
        }
    }
}
