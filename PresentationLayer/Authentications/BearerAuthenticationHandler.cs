using System.Text.Encodings.Web;
using DomainLayer.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;

namespace PresentationLayer.Authentications;

public class BearerAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{

    private readonly IPasswordHasher _passwordHasher;
    
    public BearerAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, 
        ILoggerFactory logger, UrlEncoder encoder,IPasswordHasher passwordHasher) : base(options, logger, encoder)
    {
        _passwordHasher = passwordHasher;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (!Request.Headers.ContainsKey("Authentication"))
            return Task.FromResult(AuthenticateResult.NoResult());
        var authHeader = Request.Headers["Authentication"];
        if (!authHeader.ToString().StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            return Task.FromResult(AuthenticateResult.Fail("Invalid Scheme"));
        
        return Task.FromResult(AuthenticateResult.NoResult());
    }
}