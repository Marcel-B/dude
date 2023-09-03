using Duende.IdentityServer.Models;
using IdentityModel;

namespace com.b_velop.IdentityCat.Service;

public static class IdentityCatConfigExtensions
{
    public static IEnumerable<IdentityResource> GetIdentityResources(
        this IdentityCatConfiguration configuration)
    {
        return new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new()
            {
                Name = "verification",
                UserClaims = new List<string>
                {
                    JwtClaimTypes.Email,
                    JwtClaimTypes.EmailVerified
                }
            }
        };
    }

    public static IEnumerable<ApiScope> GetApiScopes(
        this IdentityCatConfiguration configuration)
    {
        return configuration.ApiScopes.Select(
            apiScope => new ApiScope(apiScope));
    }

    public static IEnumerable<Client> GetClients(
        this IdentityCatConfiguration configuration)
    {
        return configuration.Clients;
    }
}