using Microsoft.Extensions.DependencyInjection;

namespace DevIt.Repository;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddRepository(this IServiceCollection services)
  {
    services.AddScoped<IProjektRepository, ProjektRepository>();
    return services;
  }
}
