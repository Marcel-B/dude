using System.Globalization;
using com.b_velop.Mqtt.Domain;
using com.b_velop.Mqtt.Repository;
using Mqtt.Measurement.Adapter.Command;
using Mqtt.Shared;

namespace Mqtt.Measurement.Adapter.Handler;

public class CreateMeasurementCommandHandler : ICommandHandler<CreateMeasurementCommand>
{
    private readonly IDeviceRepository _deviceRepository;
    private readonly IMeasurementRepository _measurementRepository;
    private readonly ISensorRepository _sensorRepository;
    private readonly ICoordinateRepository _coordinateRepository;
    private readonly ITimestampRepository _timestampRepository;
    private readonly IUnitRepository _unitRepository;

    public CreateMeasurementCommandHandler(
        IMeasurementRepository measurementRepository,
        ITimestampRepository timestampRepository,
        IDeviceRepository deviceRepository,
        IUnitRepository unitRepository,
        ISensorRepository sensorRepository,
        ICoordinateRepository coordinateRepository)
    {
        _measurementRepository = measurementRepository;
        _timestampRepository = timestampRepository;
        _sensorRepository = sensorRepository;
        _coordinateRepository = coordinateRepository;
        _deviceRepository = deviceRepository;
        _unitRepository = unitRepository;
    }

    public async Task HandleAsync(
        CreateMeasurementCommand command,
        CancellationToken cancellationToken = default)
    {
        var topic = command.Topic;
        var value = command.Value;

        var tokens = topic.Split('/');

        var deviceName = tokens[0];
        var sensorName = tokens[1];
        var unitName = tokens[2];

        var gpsValue = string.Empty;
        var isGps = unitName == "GPS";
        if (isGps)
        {
            await _coordinateRepository.InsertAsync(new Coordinate
            {
                Id = DateTimeOffset.Now,
                Value =  value,
            }, cancellationToken);
            return;
        }

        var device = await _deviceRepository.GetByNameAsync(deviceName, cancellationToken) ??
                     await _deviceRepository.InsertAsync(Device.Create(deviceName), cancellationToken);

        var unit = await _unitRepository.GetByNameAsync(unitName, cancellationToken);

        var sensor = await _sensorRepository.GetSensorAsync(sensorName, device.Id, unit.Id, cancellationToken) ??
                     await _sensorRepository.InsertAsync(Sensor.Create(sensorName, device, unit), cancellationToken);


        if (!double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out var measurementValue))
        {
            throw new ArgumentException("Value is not a valid double", nameof(value));
        }

        var now = DateTimeOffset.Now;
        var currentTimestamp = new DateTimeOffset(now.Year, now.Month, now.Day, now.Hour, now.Minute, 0, now.Offset);
        var timestamp = await _timestampRepository.GetByDateTimeAsync(currentTimestamp, cancellationToken) ??
                        await _timestampRepository.InsertAsync(Timestamp.Create(currentTimestamp), cancellationToken);

        await _measurementRepository.InsertAsync(
            com.b_velop.Mqtt.Domain.Measurement.Create(sensor, measurementValue, timestamp),
            cancellationToken);
    }
}