

using CQRS.Api.Common.Errors;
using CQRS.Api.Common.Mapping;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace CQRS.Api;

public static class DependencyInjection{

    public static IServiceCollection AddPresentation(this IServiceCollection services){

        services.AddControllers();
        services.AddSingleton<ProblemDetailsFactory, CQRSProblemDetailsFactory>();
        services.AddMappings();
        return services;

    }
}