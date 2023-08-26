using Microsoft.Extensions.DependencyInjection;
using Mqtt.Measurement.Adapter.Command;
using Mqtt.Measurement.Adapter.Handler;
using Mqtt.Shared;

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