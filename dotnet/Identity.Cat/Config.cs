using Duende.IdentityServer.Models;

namespace Identity.Cat;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile()
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
            new("scope1"),
            new("scope2"),
            new("pbi_read"),
            new("pbi_write"),
            new("pbi_admin"),
        };

    public static IEnumerable<Client> Clients =>
        new Client[]
        {
            // m2m client credentials flow client
            new()
            {
                ClientId = "m2m.client",
                ClientName = "Client Credentials Client",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets = {new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256())},
                AllowedScopes = {"scope1"}
            },
            new()
            {
                ClientId = "pbi.admin",
                ClientName = "Client for PBI Application",
                AllowedGrantTypes = GrantTypes.Code,
                ClientSecrets = {new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256())},
                RedirectUris = {"https://marcelbenders.com/signin-oidc"},
                AllowedCorsOrigins = {"https://marcelbenders.com"},
                FrontChannelLogoutUri = "https://marcelbenders.com/signout-oidc",
                PostLogoutRedirectUris = {"https://marcelbenders.com/signout-callback-oidc"},
                RequirePkce = true,
                RequireClientSecret = false,
                AllowOfflineAccess = true,
                AllowedScopes = {"openid", "profile", "pbi_admin", "pbi_write", "pbi_read"}
            }
        };
}