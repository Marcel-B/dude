using Microsoft.Extensions.DependencyInjection;

namespace IdentityCat.Application;

public static class IdentityServerBuilderExtensions
{
    /// <summary>
    /// Adds test users.
    /// </summary>
    /// <param name="builder">The builder.</param>
    /// <param name="users">The users.</param>
    /// <returns></returns>
    public static IIdentityServerBuilder AddUserStore(
        this IIdentityServerBuilder builder)
    {
        builder.Services.AddScoped<IUserStore, UserStore>();
        builder.AddProfileService<UserProfileService>();
        builder.AddResourceOwnerValidator<UserResourceOwnerPasswordValidator>();
        builder.AddBackchannelAuthenticationUserValidator<BackchannelLoginUserValidator>();
        return builder;
    }
}