using DevIt.IntegrationTests.Extensions.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NSubstitute;

namespace DevIt.IntegrationTests;

public class IntegrationTestsFixture : WebApplicationFactory<Program>
{
    public string Environment { get; set; } = "IntegrationTests";
    public Func<HttpContext, AuthenticateResult> OnHandleAuthenticate { get; set; }
    public readonly ITestFooClient TestFooClient;

    public IntegrationTestsFixture()
    {
      TestFooClient = Substitute.For<ITestFooClient>();

        OnHandleAuthenticate = ctx =>
        {
            var authTicket = ClaimsExtensions
                .CreateClaims("User", "user_prod", "devit")
                .CreateAuthenticationTicket();
            return AuthenticateResult.Success(authTicket);
        };
    }

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder
            .ConfigureServices((context, services) =>
            {
                var testFooClient = services.SingleOrDefault(
                    d => d.ServiceType ==
                         typeof(ITestFooClient));
                services.Remove(testFooClient);
                services.AddSingleton(TestFooClient);
            })
            .ConfigureTestAuthentication("TestAuth", OnHandleAuthenticate);

        base.ConfigureWebHost(builder.UseEnvironment(Environment));
    }
}
