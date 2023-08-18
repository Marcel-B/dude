using Microsoft.Extensions.DependencyInjection;

namespace Identity.Servus.AppUser.Adapter;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDomain(
        this IServiceCollection services)
    {
        return services;
    }
}