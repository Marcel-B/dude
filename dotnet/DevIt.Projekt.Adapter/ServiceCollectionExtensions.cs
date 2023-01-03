using DevIt.Projekt.Adapter.Command;
using DevIt.Projekt.Adapter.Handler;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevIt.Projekt.Adapter;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddProjektAdapter(this IServiceCollection services)
  {
    services.AddScoped<IRequestHandler<CreateProjektCommand>, CreateProjektCommandHandler>();
    return services;
  }
}
