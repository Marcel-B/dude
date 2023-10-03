using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Servus.Persistence;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddDomain(this IServiceCollection services)
  {
    var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
    return services;
  }
}
