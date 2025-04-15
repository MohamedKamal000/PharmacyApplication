using System.Security.Claims;

namespace DomainLayer.Interfaces;

public interface ITokenGenerator
{
    string GenerateToken(ClaimsIdentity claims);
}