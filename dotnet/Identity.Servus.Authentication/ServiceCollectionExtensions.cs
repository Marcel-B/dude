using Identity.Servus.Authentication.Handler;
using Identity.Servus.Domain;
using Identity.Servus.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Identity.Servus.Authentication;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuthN(
        this IServiceCollection serviceCollection)
    {
        serviceCollection
            .AddMediatR(typeof(CreateAppUserCommandHandler))
            .AddIdentity<AppUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        serviceCollection.Configure<IdentityOptions>(options =>
        {
            // Password settings.
            options.Password.RequireDigit = true;
            options.Password.RequireLowercase = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 1;

            // Lockout settings.
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(5);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // User settings.
            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            options.User.RequireUniqueEmail = true;
        });

        // serviceCollection.ConfigureApplicationCookie(options =>
        // {
        //   // Cookie settings
        //   options.Cookie.HttpOnly = true;
        //   options.ExpireTimeSpan = TimeSpan.FromDays(5);
        //   options.LoginPath = "/Identity/Account/Login";
        //   options.AccessDeniedPath = "/Identity/Account/AccessDenied";
        // });

        serviceCollection
            .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie(options =>
            {
                options.AccessDeniedPath = "/connect/signin";
                options.LoginPath = "/Identity/Account/Login";
                options.LogoutPath = "/Identity/Account/Logout";
                options.SlidingExpiration = true;
                options.ExpireTimeSpan = TimeSpan.FromDays(5);
            });

        return serviceCollection;
    }
}