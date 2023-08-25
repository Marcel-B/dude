using com.b_velop.Mqtt.Domain;

namespace com.b_velop.Mqtt.Measurement.Service.Mapper;

internal static class SensorSerivceExtensions
{
    public static Device ToSystem(
        this DeviceDto deviceDto)
    {
        return new Device
        {
            Id = deviceDto.Id.ToSystem(),
            Name = deviceDto.Name,
        };
    }

    public static DeviceDto ToProto(
        this Device device)
    {
        return new DeviceDto
        {
            Id = device.Id.ToProto(),
            Name = device.Name,
        };
    }

    public static Sensor ToSystem(
        this SensorDto sensorDto)
    {
        return new Sensor
        {
            Id = sensorDto.Id.ToSystem(),
            Name = sensorDto.Name,
        };
    }

    public static SensorDto ToProto(
        this Sensor sensor)
    {
        return new SensorDto
        {
            Id = sensor.Id.ToProto(),
            Name = sensor.Name,
        };
    }
}