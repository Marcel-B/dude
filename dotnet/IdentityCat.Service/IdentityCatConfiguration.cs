using Duende.IdentityServer.Models;

namespace com.b_velop.IdentityCat.Service;

public record IdentityCatConfiguration
{
    public string IdentityServerHost { get; init; } = string.Empty;
    public IEnumerable<string> ApiScopes { get; init; } = new List<string>();
    public IEnumerable<Client> Clients { get; init; } = new List<Client>();
}