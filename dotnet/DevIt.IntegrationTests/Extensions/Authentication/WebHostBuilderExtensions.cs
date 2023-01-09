using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DevIt.IntegrationTests.Extensions.Authentication;

public static class WebHostBuilderExtensions
{
    public static void ConfigureTestAuthentication(
        this IWebHostBuilder builder,
        string testAuthenticationScheme,
        Func<HttpContext, AuthenticateResult> onHandleAuthenticate)
    {
        builder.ConfigureServices(services =>
        {
            services.ConfigureTestAuthentication(testAuthenticationScheme,
                onHandleAuthenticate);
        });
    }

    private static IServiceCollection ConfigureTestAuthentication(
        this IServiceCollection services,
        string testAuthenticationScheme,
        Func<HttpContext, AuthenticateResult> onHandleAuthenticate)
    {
        services
            .AddOptions<TestAuthHandlerOptions>()
            .Configure(options => options.OnHandleAuthenticate = onHandleAuthenticate);

        services
            .AddAuthentication(testAuthenticationScheme)
            .AddScheme<TestAuthHandlerOptions, TestAuthHandler>(testAuthenticationScheme, null);

        return services;
    }

    private class TestAuthHandlerOptions : AuthenticationSchemeOptions
    {
        public Func<HttpContext, AuthenticateResult>? OnHandleAuthenticate { get; set; }
    }

    private class TestAuthHandler : AuthenticationHandler<TestAuthHandlerOptions>
    {
        IOptionsMonitor<TestAuthHandlerOptions> _options;

        public TestAuthHandler(IOptionsMonitor<TestAuthHandlerOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
            _options = options;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var result = _options.CurrentValue.OnHandleAuthenticate(Context);
            return Task.FromResult(result);
        }
    }
}
