using DevIt.Persistence;
using DevIt.Projekt.Adapter;
using DevIt.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace DevIt.Application;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddApplication(this IServiceCollection services)
  {
    services
      .AddProjektAdapter()
      .AddPersistence()
      .AddRepository();
    return services;
  }
}
