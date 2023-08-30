namespace DevIt.IntegrationTests.Controllers
{
  [Collection("ApiTestFixtureCollection")]
  public class AbrechnungControllerTests : IClassFixture<IntegrationTestsFixture>
  {
    private const string UserId = "Norbert Kahl";
    private readonly HttpClient _client;
    private readonly IServiceProvider _serviceProvider;

    public AbrechnungControllerTests(IntegrationTestsFixture apiTestsFixture)
    {
      // _serviceProvider = apiTestsFixture.Services;
      // _client = apiTestsFixture
      //     .ConfigureAuth(UserId)
      //     .CreateClient(new WebApplicationFactoryClientOptions
      //     {
      //         BaseAddress = new Uri("http://localhost/api/"),
      //         AllowAutoRedirect = false
      //     });
    }

    [Fact]
    public async Task TutDinge()
    {
      // Arrange
      // var id = "1234";
      // var expected = File.ReadAllText("./Controllers/__snapshots__/foo.json");
      //
      // // Act
      // var response = await _client.GetAsync($"abrechnung/by-month");
      //
      // // Assert
      // response.StatusCode.Should().Be(HttpStatusCode.OK);
      // var jsonString = await response.Content.ReadAsStringAsync();
      // jsonString.Should().BeEquivalentTo(expected);
    }
  }
}
