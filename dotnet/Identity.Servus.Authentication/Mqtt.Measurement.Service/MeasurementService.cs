using com.b_velop.Dude.Shared;
using com.b_velop.Mqtt.Repository;
using Grpc.Core;

namespace com.b_velop.Mqtt.Measurement.Service;

public class MeasurementService : Dude.Shared.Measurement.MeasurementBase
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
        var g = new System.Guid(id.ToByteArray());
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

    public override async Task<GetDeviceSensorsReply> GetDeviceSensors(
        GetDeviceSensoresRequest request,
        ServerCallContext context)
    {
        var device = await _deviceRepository.GetByIdAsync(request.DeviceId.ToSystem(), context.CancellationToken);
        var reply = new GetDeviceSensorsReply
        {
            Name = device.Name,
            Id = device.Id.ToProto(),
            Sensors = {device.Sensors.Select(x => x.ToProto())}
        };
        return reply;
    }
}