using com.b_velop.Dude.Shared;
using com.b_velop.Measurement.UiModel;
using Google.Protobuf;

namespace com.b_velop.Measurement.Services;

public record Sensor(
    System.Guid Id,
    string Name);

public record Device(
    System.Guid Id,
    string Name,
    IEnumerable<Sensor> Sensoren);

public class MeasurementService
{
    private readonly Dude.Shared.Measurement.MeasurementClient _measurementClient;

    public MeasurementService(
        Dude.Shared.Measurement.MeasurementClient measurementClient)
    {
        _measurementClient = measurementClient;
    }

    public async Task<double> GetMeasurement(
        System.Guid id,
        CancellationToken cancellationToken = default)
    {
        var request = new GetMeasurementRequest
        {
            SensorId = new Dude.Shared.Guid
            {
                Value =
                    ByteString.CopyFrom(id.ToByteArray())
            }
        };
        var reply = await _measurementClient.GetMeasurementAsync(request, cancellationToken: cancellationToken);

        return reply.Values;
    }

    public async Task<IEnumerable<SelectItem>> GetSensors(
        CancellationToken cancellationToken = default)
    {
        var request = new GetSensorsRequest();
        var reply = await _measurementClient.GetSensorsAsync(request, cancellationToken: cancellationToken);
        return reply.Sensors.Select(x => new SelectItem(x.Id.ToSystem(), x.Name));
    }

    public async Task<IEnumerable<SelectItem>> GetDevices(
        CancellationToken cancellationToken = default)
    {
        var request = new GetDevicesRequest();
        var reply = await _measurementClient.GetDevicesAsync(request, cancellationToken: cancellationToken);
        return reply.Devices.Select(x => new SelectItem(x.Id.ToSystem(), x.Name));
    }

    public async Task<Device> GetDeviceById(
        System.Guid id,
        CancellationToken cancellationToken = default)
    {
        var request = new GetDeviceSensoresRequest
        {
            DeviceId = id.ToProto()
        };
        var reply = await _measurementClient.GetDeviceSensorsAsync(request, cancellationToken: cancellationToken);
        var device = new Device(
            reply.Id.ToSystem(),
            reply.Name,
            reply.Sensors.Select(x => new Sensor(x.Id.ToSystem(), x.Name)));
        return device;
    }
}