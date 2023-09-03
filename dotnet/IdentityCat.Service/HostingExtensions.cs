using System.IdentityModel.Tokens.Jwt;
using com.b_velop.IdentityCat.Service.Data;
using com.b_velop.IdentityCat.Service.Models;
using Duende.IdentityServer.Configuration;
using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.ResponseHandling;
using Duende.IdentityServer.Services;
using Duende.IdentityServer.Validation;
using IdentityCat.Persistence;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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
        builder.Services.AddPersistence(builder.Configuration);
        builder.Services.AddRazorPages();

        builder
            .Services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.Cookie.Expiration = TimeSpan.FromHours(1);
                options.Cookie.SameSite = SameSiteMode.Strict;
                options.Cookie.Name = "idsrv.trude.gerd";
            });
        var identityCatConfiguration = builder
            .Configuration.GetSection("IdentityCatConfiguration")
            .Get<IdentityCatConfiguration>();

        builder
            .Services
            .AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;

                options.Authentication.CookieSameSiteMode = SameSiteMode.Strict;
                options.Authentication.CookieLifetime = TimeSpan.FromHours(1);
                options.Authentication.CookieSlidingExpiration = false;
                //options.Authentication.CheckSessionCookieName = "idsrv.hans.gerd";

                // see https://docs.duendesoftware.com/identityserver/v6/fundamentals/resources/
                options.EmitStaticAudienceClaim = true;
            })
            .AddInMemoryIdentityResources(identityCatConfiguration.GetIdentityResources())
            .AddInMemoryApiScopes(identityCatConfiguration.GetApiScopes())
            .AddInMemoryClients(identityCatConfiguration.GetClients());

        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(
        this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        app.UseForwardedHeaders(new ForwardedHeadersOptions
        {
            ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
        });
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
            if (context.Request.Path.HasValue && context.Request.Path.Value.StartsWith("/connect/authorize"))
            {
                var request = context.Request;
                var referer = request
                    .Headers["Referer"]
                    .ToString();
            }

            // Setzen Sie den CSP-Header
            context.Response.Headers.Add("Content-Security-Policy",
                "frame-ancestors 'self' http://localhost:9000;");
            await next();
        });
        app.UseCookiePolicy(new CookiePolicyOptions {MinimumSameSitePolicy = SameSiteMode.Strict});
        // app.MapControllerRoute(
        //     name:
        //     "default",
        //     pattern:
        //     "{controller=Home}/{action=Index}/{id?}");
        // endpoints.MapControllerRoute(
        //     name: "identity",
        //     pattern: "Identity/{controller=Account}/{action=Login}/{id?}");
        if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

        app.UseStaticFiles();
        app.UseRouting();
        app.UseIdentityServer();
        app.UseAuthorization();
        app.MapRazorPages();
        return app;
    }
}