using com.b_velop.Mqtt.Measurement.Service.Mapper;
using com.b_velop.Mqtt.Repository;
using Google.Protobuf;
using Grpc.Core;

namespace com.b_velop.Mqtt.Measurement.Service;

public static class CommonExtensions
{
    public static Guid ToSystem(
        this Common.Guid guid)
    {
        return new Guid(guid.Value.ToByteArray());
    }

    public static Common.Guid ToProto(
        this Guid guid)
    {
        return new Common.Guid
        {
            Value = ByteString.CopyFrom(guid.ToByteArray())
        };
    }
}

public class MeasurementService : Measurement.MeasurementBase
{
    private readonly ISensorRepository _sensorRepository;
    private readonly IDeviceRepository _deviceRepository;
    private readonly IMeasurementRepository _measurementRepository;

    public MeasurementService(
        ISensorRepository sensorRepository,
        IDeviceRepository deviceRepository,
        IMeasurementRepository measurementRepository)
    {
        _sensorRepository = sensorRepository;
        _deviceRepository = deviceRepository;
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

    public override async Task<GetSensorsReply> GetSensors(
        GetSensorsRequest request,
        ServerCallContext context)
    {
        var sensors = await _sensorRepository.GetSensorsAsync(context.CancellationToken);
        var reply = new GetSensorsReply
        {
            Sensors = {sensors.Select(x => x.ToProto())}
        };
        return reply;
    }

    public override async Task<GetDevicesReply> GetDevices(
        GetDevicesRequest request,
        ServerCallContext context)
    {
        var devices = await _deviceRepository.GetDevicesAsync(context.CancellationToken);
        var reply = new GetDevicesReply
        {
            Devices = {devices.Select(x => x.ToProto())}
        };
        return reply;
    }
}