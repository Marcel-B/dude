using Duende.IdentityServer;
using Duende.IdentityServer.Test;
using IdentityCat.Domain;
using IdentityCat.Persistence;
using IdentityCat.UserAdapter;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace TestCat;

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
        // builder.Services.AddSingleton(new TestUserStore(users));
        builder.AddProfileService<UserProfileService>();
        builder.AddResourceOwnerValidator<UserResourceOwnerPasswordValidator>();
        builder.AddBackchannelAuthenticationUserValidator<BackchannelLoginUserValidator>();
        return builder;
    }
}

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(
        this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorPages();
        builder.Services.AddScoped<UserStore>();
        builder.Services.AddPersistence(builder.Configuration);
        builder.Services.AddUserAdapter();

        var isBuilder = builder
            .Services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                options.EmitStaticAudienceClaim = true;
            })
            .AddUserStore();

        // in-memory, code config
        isBuilder.AddInMemoryIdentityResources(Config.IdentityResources);
        isBuilder.AddInMemoryApiScopes(Config.ApiScopes);
        isBuilder.AddInMemoryClients(Config.Clients);


        // if you want to use server-side sessions: https://blog.duendesoftware.com/posts/20220406_session_management/
        // then enable it
        //isBuilder.AddServerSideSessions();
        //
        // and put some authorization on the admin/management pages
        //builder.Services.AddAuthorization(options =>
        //       options.AddPolicy("admin",
        //           policy => policy.RequireClaim("sub", "1"))
        //   );
        //builder.Services.Configure<RazorPagesOptions>(options =>
        //    options.Conventions.AuthorizeFolder("/ServerSideSessions", "admin"));


        builder
            .Services.AddAuthentication()
            .AddGoogle(options =>
            {
                options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

                // register your IdentityServer with Google at https://console.developers.google.com
                // enable the Google+ API
                // set the redirect URI to https://localhost:5001/signin-google
                options.ClientId = "copy client ID from Google here";
                options.ClientSecret = "copy client secret from Google here";
            });

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(
        this WebApplication app)
    {
        var scope = app.Services
            .CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<UserDbContext>();
        context.Database.Migrate();

        var cmd = new User.CreateUserCommand("marcel", "Pass123$");
        var user = User.Create(cmd);
        context.Users.Add(user);
        context.UserClaims.AddRange(
            new UserClaim
            {
                User = user,
                Type = "scope",
                Value = "pbi_admin"
            }, new UserClaim
            {
                User = user,
                Type = "scope",
                Value = "openid",
            }, new UserClaim
            {
                User = user,
                Type = "scope",
                Value = "profile",
            });
        context.SaveChanges();
        scope.Dispose();


        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStaticFiles();
        app.UseRouting();
        app.UseIdentityServer();
        app.UseAuthorization();

        app
            .MapRazorPages()
            .RequireAuthorization();

        return app;
    }
}