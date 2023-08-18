using DevIt.Abrechnung.Adapter;
using DevIt.Domain;
using DevIt.Eintrag.Adapter;
using DevIt.Moco.Adapter;
using DevIt.Moco.Service;
using DevIt.Pbi.Adapter;
using DevIt.Persistence;
using DevIt.Projekt.Adapter;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevIt.Application;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
  {
    var apiKey = configuration["Moco:ApiKey"];
    var url = configuration["Moco:Url"];

    services.AddMocoService(apiKey ?? throw new InvalidOperationException("ApiKey is null"), url ?? throw new InvalidOperationException("Url is null"));

    return services
      .AddDomain()
      .AddEintragAdapter()
      .AddProjektAdapter()
      .AddAbrechnungAdapter()
      .AddMocoAdapter()
      .AddPbiAdapter()
      .AddPersistence();
  }
}
