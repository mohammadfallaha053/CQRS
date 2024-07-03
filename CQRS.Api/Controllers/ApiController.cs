using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ErrorOr;
using CQRS.Api.Common.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Authorization;
namespace CQRS.Api.Controllers;
[ApiController]
[Authorize]
public class ApiController : ControllerBase
{

    protected IActionResult Problem(List<Error> errors)
    {
        if (errors.Count is 0)
        {
            return Problem();
        }


        if (errors.All(error => error.Type == ErrorType.Validation))
        {
            return ValidationProblem(errors);
        }



        HttpContext.Items[HttpContextItemKeys.Errors] = errors;

        var firstError = errors[0];

        return Problem(firstError);

    }




    private IActionResult  Problem(Error error) {
        
        var statusCode = error.Type switch
        {
            ErrorType.Conflict => StatusCodes.Status409Conflict,
            ErrorType.Validation => StatusCodes.Status400BadRequest,
            ErrorType.NotFound => StatusCodes.Status404NotFound,
            _=> StatusCodes.Status500InternalServerError,
        };
        return Problem(statusCode: statusCode,title: error.Description);
    }




    private IActionResult ValidationProblem(List<Error> errors)
    {
    
        var modelStateDictinoary = new ModelStateDictionary();
        foreach (var error in errors)
        {
            modelStateDictinoary.AddModelError(
                error.Code,
                error.Description
                );
        }


        return ValidationProblem(modelStateDictinoary);

    
    }


}




