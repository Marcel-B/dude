using com.b_velop.Mqtt.Domain;

namespace com.b_velop.Mqtt.Repository;

public interface IUnitRepository
{
    Task<Unit> GetByNameAsync(
        string name,
        CancellationToken cancellationToken = default);
}