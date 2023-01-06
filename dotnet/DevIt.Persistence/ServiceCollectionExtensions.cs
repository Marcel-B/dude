using DevIt.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevIt.Persistence;

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddPersistence(this IServiceCollection services)
  {
    {
      var sp = services.BuildServiceProvider();
      var configuration = sp.GetRequiredService<IConfiguration>();

      services.AddDbContextFactory<ApplicationContext>(options =>
        options.UseSqlServer(configuration.GetConnectionString("SqlServer")));
      services.AddScoped<IUnitOfWork, UnitOfWork>();

      return services;
    }
  }
}
