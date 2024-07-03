using CQRS.Application.Authentication.Common;
using CQRS.Contracts.Authentication;
using Mapster;
using static CQRS.Domain.Common.Errors.Errors;

namespace CQRS.Api.Common.Mapping;

public class AuthenticationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AuthenticationResult, AuthenticationResponse>()
            .Map(dest => dest.Token, src => src.Token)
            .Map(dest => dest, src => src.User);
    }
}
