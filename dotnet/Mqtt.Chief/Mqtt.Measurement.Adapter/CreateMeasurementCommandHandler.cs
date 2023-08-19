using System.Globalization;
using Mqtt.Domain;
using Mqtt.Repository;

namespace Mqtt.Measurement.Adapter;

public class CreateMeasurementCommandHandler : ICommandHandler<CreateMeasurementCommand>
{
  private readonly IMeasurementRepository _measurementRepository;
  private readonly ITimestampRepository _timestampRepository;
  private readonly ISensorRepository _sensorRepository;
  private readonly IDeviceRepository _deviceRepository;
  private readonly IUnitRepository _unitRepository;

  public CreateMeasurementCommandHandler(
    IMeasurementRepository measurementRepository,
    ITimestampRepository timestampRepository,
    IDeviceRepository deviceRepository,
    IUnitRepository unitRepository,
    ISensorRepository sensorRepository)
  {
    _measurementRepository = measurementRepository;
    _timestampRepository = timestampRepository;
    _sensorRepository = sensorRepository;
    _deviceRepository = deviceRepository;
    _unitRepository = unitRepository;
  }

  public async Task HandleAsync(CreateMeasurementCommand command, CancellationToken cancellationToken = default)
  {
    var topic = command.Topic;
    var value = command.Value;

    var tokens = topic.Split('/');

    var deviceName = tokens[0];
    var sensorName = tokens[1];
    var unitName = tokens[2];

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

    await _measurementRepository.InsertAsync(Domain.Measurement.Create(sensor, measurementValue, timestamp),
      cancellationToken);
  }
}
