using com.b_velop.IdentityCat.Service.Data;
using com.b_velop.IdentityCat.Service.Models;
using Duende.IdentityServer;
using Duende.IdentityServer.Services;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace com.b_velop.IdentityCat.Service;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(
        this WebApplicationBuilder builder)
    {
        builder.Services.AddRazorPages();
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerTitan")));
        builder.Services.AddScoped<IEmailSender, EmailSender>();
        builder
            .Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        var identityCatConfiguration = builder
            .Configuration.GetSection("IdentityCatConfiguration")
            .Get<IdentityCatConfiguration>();

        builder
            .Services
            .AddIdentityServer(options =>
            {
                options.Authentication.CheckSessionCookieName = "idsrv.hans.gerd";
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
                options.Authentication.CookieLifetime = TimeSpan.FromHours(1);
                options.Authentication.CookieSlidingExpiration = false;
                options.Authentication.CookieSameSiteMode = SameSiteMode.Strict;
                // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                options.EmitStaticAudienceClaim = true;
            })
            .AddInMemoryIdentityResources(identityCatConfiguration.GetIdentityResources())
            .AddInMemoryApiScopes(identityCatConfiguration.GetApiScopes())
            .AddInMemoryClients(identityCatConfiguration.GetClients())
            .AddCorsPolicyService<InMemoryCorsPolicyService>() // Eigener CORS-Dienst
            .AddAspNetIdentity<ApplicationUser>();
        builder.Services.AddSingleton<ICorsPolicyService>(options =>
        {
            var logger = options.GetRequiredService<ILogger<DefaultCorsPolicyService>>();

            return new DefaultCorsPolicyService(logger)
            {
                AllowedOrigins = {"http://localhost:9000", "https://idsrv.marcelbenders.com"}
            };
        });

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
        app.UseSerilogRequestLogging();
        app.Use((
            context,
            next) =>
        {
            context.Request.Scheme = "https";
            return next();
        });
        app.Use(async (
            context,
            next) =>
        {
            // Setzen Sie den CSP-Header
            context.Response.Headers.Add("Content-Security-Policy",
                "frame-ancestors 'self' http://localhost:9000;");
            await next();
        });
        app.UseCookiePolicy(new CookiePolicyOptions {MinimumSameSitePolicy = SameSiteMode.Strict});

        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });
        if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

        app.UseStaticFiles();
        app.UseRouting();
        app.UseIdentityServer();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            "default",
            "{controller=Home}/{action=Index}/{id?}");

        app.MapRazorPages();

        return app;
    }
}