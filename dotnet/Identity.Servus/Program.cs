using Identity.Servus;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using OpenIddict.Server;
using Quartz;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddRazorPages();
services.AddScoped<IEmailSender, EmailSender>();

services.AddDbContext<ApplicationContext>(options =>
{
  // Configure the context to use an in-memory store.
  options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
  // Register the entity sets needed by OpenIddict.
  options.UseOpenIddict();
});

builder
  .Services
  .AddIdentity<AppUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
  .AddEntityFrameworkStores<ApplicationContext>()
  .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
  // Password settings.
  options.Password.RequireDigit = true;
  options.Password.RequireLowercase = true;
  options.Password.RequireNonAlphanumeric = true;
  options.Password.RequireUppercase = true;
  options.Password.RequiredLength = 6;
  options.Password.RequiredUniqueChars = 1;

  // Lockout settings.
  options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
  options.Lockout.MaxFailedAccessAttempts = 5;
  options.Lockout.AllowedForNewUsers = true;

  // User settings.
  options.User.AllowedUserNameCharacters =
    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
  options.User.RequireUniqueEmail = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
  // Cookie settings
  options.Cookie.HttpOnly = true;
  options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

  options.LoginPath = "/Identity/Account/Login";
  options.AccessDeniedPath = "/Identity/Account/AccessDenied";
  options.SlidingExpiration = true;
});

services
  .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
  .AddCookie(options =>
  {
    options.AccessDeniedPath = "/connect/signin";
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
  });


// OpenIddict offers native integration with Quartz.NET to perform scheduled tasks
// (like pruning orphaned authorizations/tokens from the database) at regular intervals.
services.AddQuartz(options =>
{
  options.UseMicrosoftDependencyInjectionJobFactory();
  options.UseSimpleTypeLoader();
  options.UseInMemoryStore();
});

// Register the Quartz.NET service and configure it to block shutdown until jobs are complete.
services.AddQuartzHostedService(options => options.WaitForJobsToComplete = true);

// Register the OpenIddict services.
services.AddOpenIddict()
  .AddCore(options =>
  {
    // Register the Entity Framework Core models/stores.
    options.UseEntityFrameworkCore()
      .UseDbContext<ApplicationContext>();

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
        "http://localhost:5210",
        //"http://192.168.2.103:6065"
      };
    });

    // Register the event handler responsible for populating userinfo responses.
    options.AddEventHandler<OpenIddictServerEvents.HandleUserinfoRequestContext>(options =>
      options.UseSingletonHandler<Handlers.PopulateUserinfo>());
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

// Register the worker responsible for creating and seeding the SQL database.
// Note: in a real world application, this step should be part of a setup script.
services.AddHostedService<Worker>();

var app = builder.Build();

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(options => options.MapRazorPages());

app.Run();
