using com.b_velop.Measurement.Service;
using Google.Protobuf;

namespace com.b_velop.Measurement.Services;

// public class MeasurementClient : Measurement.MeasurementClient
// {
//     private readonly ILogger<MeasurementClient> _logger;
//
//     public MeasurementClient(
//         ILogger<MeasurementClient> logger)
//     {
//         _logger = logger;
//     }
// }

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
}