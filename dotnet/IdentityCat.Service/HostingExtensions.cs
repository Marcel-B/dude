using Duende.IdentityServer.Extensions;
using IdentityCat.Application;
using IdentityCat.Persistence;
using IdentityCat.UserAdapter;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace com.b_velop.IdentityCat.Service;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(
        this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorPages();
        builder.Services.AddPersistence(builder.Configuration);
        builder.Services.AddUserAdapter();

        var identityCatConfiguration = builder
            .Configuration.GetSection("IdentityCatConfiguration")
            .Get<IdentityCatConfiguration>() ?? throw new NullReferenceException("IdentityCatConfiguration is null");

        _ = builder
            .Services.AddIdentityServer(options =>
            {
                options.Authentication.CookieSameSiteMode = SameSiteMode.None;
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                options.EmitStaticAudienceClaim = true;
            })
            .AddUserStore()
            .AddInMemoryIdentityResources(identityCatConfiguration.GetIdentityResources())
            .AddInMemoryApiScopes(identityCatConfiguration.GetApiScopes())
            .AddInMemoryClients(identityCatConfiguration.GetClients());

        builder
            .Services.AddAuthentication();

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(
        this WebApplication app)
    {
        using var scope = app
            .Services
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();
        var dbContext = scope.ServiceProvider.GetService<UserDbContext>();
        dbContext.Database.Migrate();
        dbContext.Dispose();
        scope.Dispose();

        app.Use(async (
            ctx,
            next) =>
        {
            ctx.SetIdentityServerOrigin("https://idsrv.marcelbenders.com");
            await next();
        });
        app.UseSerilogRequestLogging();
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });

        app.UseCookiePolicy(new CookiePolicyOptions {MinimumSameSitePolicy = SameSiteMode.None});
        if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

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