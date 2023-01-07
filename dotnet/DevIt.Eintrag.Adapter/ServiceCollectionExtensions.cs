using DevIt.Eintrag.Adapter.Handler;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevIt.Eintrag.Adapter;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddEintragAdapter(this IServiceCollection services)
  {
    services.AddMediatR(typeof(CreateEintragCommandHandler));
    return services;
  }
}
