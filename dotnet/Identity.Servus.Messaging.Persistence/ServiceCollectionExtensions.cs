using Microsoft.Extensions.DependencyInjection;

namespace Identity.Servus.Messaging.Persistence;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMessaginDb(
        this IServiceCollection services)
    {
        return services;
    }
}