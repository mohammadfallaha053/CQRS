using CQRS.Application.Common.Errors;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Api.Controllers;



public class ErrorController : ControllerBase
{
    [Route("/error")]
    public IActionResult Error()
    {
        Exception? exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;

        var (statusCode, message) = exception switch
        {
            IServiceException serviceException => ((int)serviceException.statusCodes, serviceException.Errormessage),
            _ => (StatusCodes.Status500InternalServerError, "An Error from 053"),
        };

        return Problem(statusCode:statusCode,title:message);
    }

}
