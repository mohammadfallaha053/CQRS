using CQRS.Api.Controllers;
using CQRS.Application.Authentication.Commands.Register;
using CQRS.Application.Authentication.Common;
using CQRS.Application.Authentication.Queries.Login;
using CQRS.Contracts.Authentication;
using CQRS.Domain.Common.Errors;
using ErrorOr;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Api.Controller;
[Route("auth")]
[AllowAnonymous]
public class AuthenticationController : ApiController

{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public AuthenticationController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost("register")]
    public async Task<IActionResult>  Register(RegisterRequest request){

       
        var command =_mapper.Map<RegisterCommand>(request);

        ErrorOr <AuthenticationResult> authResult=await _mediator.Send(command);


        return authResult.Match(
            authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
            errors => Problem(errors)
            );

    }

    [HttpPost("login")]
    public async Task<IActionResult>  Login(LoginRequest request){
        var query =_mapper.Map<LoginQuery>(request);
       
        var authResult =await _mediator.Send(query);
          

        if(authResult.IsError && authResult.FirstError == Errors.Authentication.InvalidCredentials)
        {
            return Problem(statusCode: StatusCodes.Status401Unauthorized,
                           title:authResult.FirstError.Description);

        }


        return authResult.Match(
              authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
              errors => Problem(errors)
              );

    }




    //private static AuthenticationResponse MapAuthResult(AuthenticationResult authResult)
    //{
    //    return new AuthenticationResponse(
    //           authResult.user.Id,
    //           authResult.user.FirstName,
    //           authResult.user.LastName,
    //           authResult.user.Email,
    //           authResult.Token);
    //}

}