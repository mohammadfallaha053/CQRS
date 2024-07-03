using CQRS.Domain.Entities;

namespace CQRS.Application.Authentication.Common;

public record AuthenticationResult(
    User User,
    string Token
);