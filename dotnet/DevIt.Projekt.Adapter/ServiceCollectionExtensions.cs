using DevIt.Projekt.Adapter.Handler;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevIt.Projekt.Adapter;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddProjektAdapter(this IServiceCollection services)
  {
    services.AddMediatR(typeof(CreateProjektCommandHandler));
    return services;
  }
}
