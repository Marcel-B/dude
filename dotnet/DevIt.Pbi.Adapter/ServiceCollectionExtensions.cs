using DevIt.Pbi.Adapter.Handler;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevIt.Pbi.Adapter;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddPbiAdapter(this IServiceCollection services)
  {
    services.AddMediatR(typeof(CreatePbiCommandHandler));
    return services;
  }
}
