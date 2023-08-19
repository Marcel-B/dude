using Mqtt.Domain;

namespace Mqtt.Repository;

public interface ISensorRepository
{
  Task<Sensor?> GetSensorAsync(string name, Guid deviceId, Guid unitId, CancellationToken cancellationToken = default);
  Task<Sensor> InsertAsync(Sensor sensor, CancellationToken cancellationToken = default);
}