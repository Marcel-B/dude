using Identity.Servus.Persistence;
using OpenIddict.Abstractions;

namespace Identity.Servus;

public class Worker : IHostedService
{
  private readonly IConfiguration _configuration;
  private readonly IServiceProvider _serviceProvider;

  public Worker(
    IConfiguration configuration,
    IServiceProvider serviceProvider)
  {
    _configuration = configuration;
    _serviceProvider = serviceProvider;
  }

  public async Task StartAsync(CancellationToken cancellationToken)
  {
    await using var scope = _serviceProvider.CreateAsyncScope();

    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    await context.Database.EnsureCreatedAsync(cancellationToken);

    var manager = scope.ServiceProvider.GetRequiredService<IOpenIddictApplicationManager>();

    // Retrieve the client definitions from the configuration
    // and insert them in the applications table if necessary.
    var descriptors = _configuration.GetSection("OpenIddict:Clients").Get<OpenIddictApplicationDescriptor[]>();
    if (descriptors.Length == 0)
    {
      throw new InvalidOperationException("No client application was found in the configuration file.");
    }

    foreach (var descriptor in descriptors)
    {
      if (await manager.FindByClientIdAsync(descriptor.ClientId!, cancellationToken) is not null)
      {
        continue;
      }

      await manager.CreateAsync(descriptor);
    }
  }

  public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
}
