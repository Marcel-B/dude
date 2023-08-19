using Duende.IdentityServer.Models;

namespace Identity.Cat;

public record IdentityCatConfiguration
{
    public IEnumerable<string> ApiScopes { get; init; } = new List<string>();
    public IEnumerable<Client> Clients { get; init; } = new List<Client>();
}