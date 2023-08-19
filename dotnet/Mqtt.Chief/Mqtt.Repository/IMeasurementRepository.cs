using Mqtt.Domain;

namespace Mqtt.Repository;

public interface IMeasurementRepository
{
  Task<Measurement> InsertAsync(Measurement measurement, CancellationToken cancellationToken = default);
}
