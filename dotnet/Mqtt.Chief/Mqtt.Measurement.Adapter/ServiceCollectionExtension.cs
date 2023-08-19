using Microsoft.Extensions.DependencyInjection;

namespace Mqtt.Measurement.Adapter;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddCommandHandlers(
        this IServiceCollection services)
    {
        services.AddScoped<ICommandHandler<CreateMeasurementCommand>, CreateMeasurementCommandHandler>();
        return services;
    }
}