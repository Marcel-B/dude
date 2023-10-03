using Identity.Servus.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OpenIddict.Server;
using Quartz;

namespace Identity.Servus.Authorization;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddAuthZ(this IServiceCollection serviceCollection)
  {
    var configuration = serviceCollection
      .BuildServiceProvider()
      .GetRequiredService<IConfiguration>();

    serviceCollection.AddDbContext<ApplicationDbContext>(options =>
    {
      // Configure the context to use an in-memory store.
      options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
      // Register the entity sets needed by OpenIddict.
      options.UseOpenIddict();
    });

// OpenIddict offers native integration with Quartz.NET to perform scheduled tasks
// (like pruning orphaned authorizations/tokens from the database) at regular intervals.
    serviceCollection.AddQuartz(options =>
    {
      options.UseMicrosoftDependencyInjectionJobFactory();
      options.UseSimpleTypeLoader();
      options.UseInMemoryStore();
    });

// Register the Quartz.NET service and configure it to block shutdown until jobs are complete.
    serviceCollection.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

// Register the OpenIddict services.
    serviceCollection.AddOpenIddict()
      .AddCore(options =>
      {
        // Register the Entity Framework Core models/stores.
        options.UseEntityFrameworkCore()
          .UseDbContext<ApplicationDbContext>();

        // Enable Quartz.NET integration.
        options.UseQuartz();
      })
      .AddServer(options =>
      {
        options
          .DisableAccessTokenEncryption();

        // Enable the authorization, token, introspection and userinfo endpoints.
        options
          .SetAuthorizationEndpointUris(configuration["OpenIddict:Endpoints:Authorization"])
          .SetTokenEndpointUris(configuration["OpenIddict:Endpoints:Token"])
          .SetIntrospectionEndpointUris(configuration["OpenIddict:Endpoints:Introspection"])
          .SetUserinfoEndpointUris(configuration["OpenIddict:Endpoints:Userinfo"]);

        // Enable the authorization code, implicit and the refresh token flows.
        options
          .AllowAuthorizationCodeFlow()
          .AllowImplicitFlow()
          .AllowRefreshTokenFlow();

        options
          .AddDevelopmentEncryptionCertificate()
          .AddDevelopmentSigningCertificate();

        // Expose all the supported claims in the discovery document.
        options.RegisterClaims(configuration.GetSection("OpenIddict:Claims").Get<string[]>());

        // Expose all the supported scopes in the discovery document.
        options.RegisterScopes(configuration.GetSection("OpenIddict:Scopes").Get<string[]>());

        // Note: an ephemeral signing key is deliberately used to make the "OP-Rotation-OP-Sig"
        // test easier to run as restarting the application is enough to rotate the keys.
        options.AddEphemeralEncryptionKey()
          .AddEphemeralSigningKey();

        // Register the ASP.NET Core host and configure the ASP.NET Core-specific options.
        //
        // Note: the pass-through mode is not enabled for the token endpoint
        // so that token requests are automatically handled by OpenIddict.
        options.UseAspNetCore()
          .EnableAuthorizationEndpointPassthrough()
          .EnableLogoutEndpointPassthrough()
          .EnableStatusCodePagesIntegration()
          .EnableTokenEndpointPassthrough()
          .EnableAuthorizationRequestCaching()
          .DisableTransportSecurityRequirement();

        options.Configure(opt =>
        {
          opt.TokenValidationParameters.ValidIssuers = new[]
          {
            configuration["OpenIddict:Authority"]
          };
        });

        // Register the event handler responsible for populating userinfo responses.
        options
          .AddEventHandler<OpenIddictServerEvents.HandleUserinfoRequestContext>(options =>
            options
              .UseSingletonHandler<Handlers.PopulateUserinfo>());
      })
      .AddValidation(options =>
      {
        // Import the configuration from the local OpenIddict server instance.
        options.UseLocalServer();

        // Register the ASP.NET Core host.
        options.UseAspNetCore();

        // Enable authorization entry validation, which is required to be able
        // to reject access tokens retrieved from a revoked authorization code.
        options.EnableAuthorizationEntryValidation();
      });

    return serviceCollection;
  }
}
