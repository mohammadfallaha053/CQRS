using CQRS.Application.Authentication.Commands.Register;
using CQRS.Application.Authentication.Common;
using ErrorOr;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CQRS.Application.Authentication.Commands.Behaviors;

public class ValidationBehavior<TRequest,TRsponse> :
    IPipelineBehavior<TRequest,TRsponse>
    where TRequest : IRequest<TRsponse> 
    where TRsponse :IErrorOr
{
   
    private readonly IValidator<TRequest>? _validator;

    public ValidationBehavior(IValidator<TRequest>? validator= null)
    {
        _validator = validator;
    }

    public async Task<TRsponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TRsponse> next,
        CancellationToken cancellationToken)
    {

        if(_validator is null)
        {
            return await next();
        }

        
            var validationResult=await _validator.ValidateAsync(request, cancellationToken);
        if (validationResult.IsValid)
        {

            return await next();
        }

        else
        {

            var errors = validationResult.Errors
                 .ConvertAll(validationFailure => Error.Validation(
                   validationFailure.PropertyName
                 , validationFailure.ErrorMessage));

            return (dynamic)errors; 
        }
    }
}
