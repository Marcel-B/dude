using Mqtt.Domain;

namespace Mqtt.Repository;

public interface IUnitRepository
{
  Task<Unit> GetByNameAsync(string name, CancellationToken cancellationToken = default);
}
