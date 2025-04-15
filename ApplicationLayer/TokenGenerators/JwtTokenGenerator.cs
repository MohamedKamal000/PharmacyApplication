using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DomainLayer.Interfaces;
using InfrastructureLayer;
using Microsoft.IdentityModel.Tokens;

namespace ApplicationLayer;

public class JwtTokenGenerator : ITokenGenerator
{

    private readonly JwtOptions _jwtOptions; 
    
    public JwtTokenGenerator(JwtOptions jwtOptions)
    {
        _jwtOptions = jwtOptions;
    }
    
    
    public string GenerateToken(ClaimsIdentity userIdentifier)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Issuer = _jwtOptions.Issuer,
            Audience = _jwtOptions.Audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SigningKey)),
                SecurityAlgorithms.HmacSha256
            ),
            Subject = userIdentifier
        };

        var secureToken = tokenHandler.CreateToken(tokenDescriptor);

        var accessToken = tokenHandler.WriteToken(secureToken);
        return accessToken;
    }

}