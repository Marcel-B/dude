using com.b_velop.Mqtt.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace com.b_velop.Mqtt.Persistence;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        return services
            .AddScoped<IDeviceRepository, DeviceRepository>()
            .AddScoped<ISensorRepository, SensorRepository>()
            .AddScoped<IUnitRepository, UnitRepository>()
            .AddScoped<ITimestampRepository, TimestampRepository>()
            .AddScoped<IMeasurementRepository, MeasurementRepository>()
            .AddDbContext<ApplicationContext>(options =>
            {
                var cs = configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(cs);
            });
    }
}