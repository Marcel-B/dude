using System.Globalization;
using com.b_velop.Dude.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace com.b_velop.Mqtt.Gps.Service;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddGpsService(
        this IServiceCollection services)
    {
        services.AddGrpc();
        return services;
    }
    // TODO - Mapper

    public static (double latitude, double longitude) ToCooridante(
        this string coordinate)
    {
        var split = coordinate.Split('?');
        return new ValueTuple<double, double>(
            double.Parse(split[0], new CultureInfo("en-US")),
            double.Parse(split[1], new CultureInfo("en-US")));
    }

    public static Coordinate ToProto(
        this com.b_velop.Mqtt.Domain.Coordinate coordinate)
    {
        var coordinates = coordinate.Value.ToCooridante();
        var time = coordinate.Id;
        return new Coordinate
        {
            Latitude = coordinates.latitude,
            Longitude = coordinates.longitude,
            Timestamp = time.ToUnixTimeMilliseconds()
        };
    }
}