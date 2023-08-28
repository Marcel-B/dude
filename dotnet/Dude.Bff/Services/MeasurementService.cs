using com.b_velop.Dude.Bff.UiModel;
using com.b_velop.Dude.Shared;
using Google.Protobuf;

namespace com.b_velop.Dude.Bff.Services;

public record Sensor(
    System.Guid Id,
    string Name,
    string Unit);

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
            reply.Sensors.Select(x => new Sensor(x.Id.ToSystem(), x.Name, x.Unit)));
        return device;
    }

    public async Task<IEnumerable<UiModel.Measurement>> GetMeasurementBySensorIdAsync(
        System.Guid sensorId,
        DateTimeOffset? from = null,
        DateTimeOffset? to = null,
        CancellationToken cancellationToken = default)
    {
        var request = new GetMeasurementsRequest
        {
            SensorId = sensorId.ToProto(),
            From = from.ToProto(),
            To = to.ToProto()
        };
        var reply = await _measurementClient.GetMeasurementsAsync(request, cancellationToken: cancellationToken);
        var result = reply.Measurements.Select(x => new UiModel.Measurement
            (x.Id.ToSystem(), x.Timestamp.ToDateTimeOffset()!.Value, x.Value));
        return result;
    }
}