using Identity.Servus.Authentication;
using Identity.Servus.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Servus.Application;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddIdentityDomain(
    this IServiceCollection services)
  {
    return services
      .AddAuthN()
      .AddAuthZ();
  }
}
