using Microsoft.Extensions.DependencyInjection;

namespace IdentityCat.UserAdapter;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddUserAdapter(
        this IServiceCollection services)
    {
        services.AddScoped<IUserAdapter, UserAdapter>();
        return services;
    }
}