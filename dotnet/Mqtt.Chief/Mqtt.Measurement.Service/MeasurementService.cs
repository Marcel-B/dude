using Grpc.Core;

namespace com.b_velop.Mqtt.Measurement.Service;

public class MeasurementService : Measurement.MeasurementBase
{
    public override Task<GetMeasurementReply> GetMeasurement(
        GetMeasurementRequest request,
        ServerCallContext context)
    {
        return base.GetMeasurement(request, context);
    }
}