using DevIt.Moco.Adapter.Handler;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevIt.Moco.Adapter;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddMocoAdapter(this IServiceCollection services)
  {
    services.AddMediatR(typeof(GetActivitiesQueryHandler));
    return services;
  }
}
