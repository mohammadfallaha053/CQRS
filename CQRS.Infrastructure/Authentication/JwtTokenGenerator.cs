
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CQRS.Application.Common.Interfaces.Services;
using CQRS.Domain.Entities;
using CQRS.Infrastructure.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace CQRS.Application.Common.interfaces.Authentication;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IDateTimeProvider _dateTimeProvider;
   

    

    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider)
    {
        _dateTimeProvider = dateTimeProvider;
       
    }

   

   public string GenerateToken(User user)
{
    var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your-32-character-secret-key-here"));
    var signingCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
    
    var claims = new[]
    {
        new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
        new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    };
    
    var securityToken = new JwtSecurityToken(
        issuer: "CQRS",
        audience: "CQRS",
        expires: _dateTimeProvider.UtcNow.AddMinutes(60),
        claims: claims,
        signingCredentials: signingCredentials
    );

    return new JwtSecurityTokenHandler().WriteToken(securityToken);
}
  
}