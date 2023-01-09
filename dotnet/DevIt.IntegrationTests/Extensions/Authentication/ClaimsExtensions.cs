using System.Security.Claims;
using IdentityModel;
using Microsoft.AspNetCore.Authentication;

namespace DevIt.IntegrationTests.Extensions.Authentication;

public static class ClaimsExtensions
{
    public static IEnumerable<Claim> CreateClaims(string name, string role, string scope)
        => new[]
        {
            new Claim(JwtClaimTypes.Name, name),
            new Claim(ClaimTypes.Role, role),
            new Claim(JwtClaimTypes.Scope, scope)
        };

    public static AuthenticationTicket CreateAuthenticationTicket(this IEnumerable<Claim> claims)
    {
        var identity = new ClaimsIdentity(claims, "Test");
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, "Test");

        return ticket;
    }
}
