using com.b_velop.Mqtt.Repository;
using Grpc.Core;

namespace com.b_velop.Mqtt.Measurement.Service;

public class MeasurementService : Measurement.MeasurementBase
{
    private readonly IMeasurementRepository _measurementRepository;

    public MeasurementService(
        IMeasurementRepository measurementRepository)
    {
        _measurementRepository = measurementRepository;
    }

    public override async Task<GetMeasurementReply> GetMeasurement(
        GetMeasurementRequest request,
        ServerCallContext context)
    {
        var id = request.SensorId?.Value ?? throw new ArgumentNullException(nameof(request.SensorId));
        var g = new Guid(id.ToByteArray());
        var measurements = await _measurementRepository.GetByIdAsync(g, context.CancellationToken);
        var measurement = new GetMeasurementReply
        {
            Values = measurements.Value
        };
        return measurement;
    }
}