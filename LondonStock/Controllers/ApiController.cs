using ErrorOr;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LondonStock.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[ApiVersion("1.0")]
public class ApiController : ControllerBase
{
    protected IActionResult Error(List<Error> errors)
    {
        if(errors.All(e => e.Type == ErrorType.Validation)){
            var dictModelState = new ModelStateDictionary();

            foreach(var error in errors){
                dictModelState.AddModelError(error.Code, error.Description);
            }

            return ValidationProblem(dictModelState);
        }

        if(errors.Any(e => e.Type == ErrorType.Unexpected)){
            return Problem();
        }

        var firstError = errors[0];
        var statusCode = firstError.Type switch{
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Unauthorized => StatusCodes.Status401Unauthorized,
            _ => StatusCodes.Status500InternalServerError
        };

        return Problem(statusCode: statusCode, title: firstError.Description);
    }
}