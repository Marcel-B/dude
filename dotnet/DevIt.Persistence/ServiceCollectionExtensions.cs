using DevIt.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DevIt.Persistence;

public class ApplicationContext : DbContext
{
  public DbSet<Projekt> Projekte { get; set; }

  public ApplicationContext(DbContextOptions<ApplicationContext> builder) : base(builder)
  {
  }
}

public static class ServiceCollectionExtensions
{
  public static IServiceCollection AddPersistence(this IServiceCollection services)
  {
    var configuration = services.BuildServiceProvider().GetRequiredService<IConfiguration>();
    services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(configuration.GetConnectionString("SqlServer")));
    return services;
  }
}
