using CQRS.Domain.Entities;

namespace CQRS.Application.Common.interfaces.Authentication;

public interface IJwtTokenGenerator{

    string GenerateToken(User user);
    
        
}