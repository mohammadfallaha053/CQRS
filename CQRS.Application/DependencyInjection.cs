using CQRS.Application.Authentication.Commands.Behaviors;
using CQRS.Application.Authentication.Commands.Register;
using CQRS.Application.Authentication.Common;
using ErrorOr;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Headers;


namespace CQRS.Application;

public static class DependencyInjection{

    public static IServiceCollection AddApplication(this IServiceCollection services){

        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly);
        });


        services.AddScoped(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>)
            );


        services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);


        return services;


    }
}