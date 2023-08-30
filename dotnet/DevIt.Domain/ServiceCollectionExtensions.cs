using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace com.b_velop.DevIt.Domain;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddDomain(this IServiceCollection services)
  {
    services.AddValidatorsFromAssemblyContaining<CreateProjekt.ProjektValidator>();
    return services;
  }
}
