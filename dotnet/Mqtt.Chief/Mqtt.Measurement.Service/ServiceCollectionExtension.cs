using Microsoft.Extensions.DependencyInjection;

namespace com.b_velop.Mqtt.Measurement.Service;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddMeasurementService(
        this IServiceCollection services)
    {
        services.AddGrpc();
        return services;
    }
}
