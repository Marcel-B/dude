using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mqtt.Repository;

namespace Mqtt.Persistence;

public static class ServiceCollectionExtension
{
  public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    => services
      .AddScoped<IDeviceRepository, DeviceRepository>()
      .AddScoped<ISensorRepository, SensorRepository>()
      .AddScoped<IUnitRepository, UnitRepository>()
      .AddScoped<ITimestampRepository, TimestampRepository>()
      .AddScoped<IMeasurementRepository, MeasurementRepository>()
      .AddDbContext<ApplicationContext>(options =>
      {
        options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
      });
}
