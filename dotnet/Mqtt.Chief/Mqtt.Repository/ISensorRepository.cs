using System.Collections;
using com.b_velop.Mqtt.Domain;

namespace com.b_velop.Mqtt.Repository;

public interface ISensorRepository
{
    Task<Sensor?> GetSensorAsync(
        string name,
        Guid deviceId,
        Guid unitId,
        CancellationToken cancellationToken = default);

    Task<ICollection<Sensor>> GetSensorsAsync(
        CancellationToken cancellationToken = default);

    Task<Sensor> InsertAsync(
        Sensor sensor,
        CancellationToken cancellationToken = default);
}