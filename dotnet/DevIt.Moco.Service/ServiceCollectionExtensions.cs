using Microsoft.Extensions.DependencyInjection;

namespace DevIt.Moco.Service;

public static class ServiceCollectionExtensions
{
  public static void AddMocoService(this IServiceCollection services, string apiKey, string url)
  {
    services.AddHttpClient<IMocoService, MocoService>(client =>
    {
      client.BaseAddress = new Uri(url);
      client.DefaultRequestHeaders.Add("Accept", "application/json");
      client.DefaultRequestHeaders.Add("Authorization", $"Token token={apiKey}");
    });
  }
}
