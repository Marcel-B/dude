using Microsoft.Extensions.DependencyInjection;

namespace Identity.Servus.Messaging;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddMessaging(this IServiceCollection serviceCollection)
  {
    return serviceCollection;
  }
}
