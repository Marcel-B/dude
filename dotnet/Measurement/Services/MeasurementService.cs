using com.b_velop.Measurement.Service;
using Google.Protobuf;

namespace com.b_velop.Measurement.Services;

public class MeasurementService
{
    private readonly Service.Measurement.MeasurementClient _measurementClient;

    public MeasurementService(
        Service.Measurement.MeasurementClient measurementClient)
    {
        _measurementClient = measurementClient;
    }

    public async Task<double> GetMeasurement(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var request = new GetMeasurementRequest
        {
            SensorId = new Service.Common.Guid
            {
                Value =
                    ByteString.CopyFrom(id.ToByteArray())
            }
        };
        var reply = await _measurementClient.GetMeasurementAsync(request, cancellationToken: cancellationToken);

        return reply.Values;
    }

    public async Task<IEnumerable<string>> GetSensors(
        CancellationToken cancellationToken = default)
    {
        var request = new GetSensorsRequest();
        var reply = await _measurementClient.GetSensorsAsync(request, cancellationToken: cancellationToken);
        return reply.Sensors.Select(x => x.Name);
    }

    public async Task<IEnumerable<string>> GetDevices(
        CancellationToken cancellationToken = default)
    {
        var request = new GetDevicesRequest();
        var reply = await _measurementClient.GetDevicesAsync(request, cancellationToken: cancellationToken);
        return reply.Devices.Select(x => x.Name);
    }
}