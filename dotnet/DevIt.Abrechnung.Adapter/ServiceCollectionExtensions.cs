using DevIt.Abrechnung.Adapter.Handler;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevIt.Abrechnung.Adapter;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddAbrechnungAdapter(this IServiceCollection services)
  {
    services.AddMediatR(typeof(GetAbrechnungByKalenderwocheQueryHandler));
    return services;
  }
}
