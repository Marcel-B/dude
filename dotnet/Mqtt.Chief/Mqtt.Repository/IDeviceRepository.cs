using Mqtt.Domain;

namespace Mqtt.Repository;

public interface IDeviceRepository
{
  Task<Device?> GetByNameAsync(string name, CancellationToken cancellationToken = default);
  Task<Device> InsertAsync(Device device, CancellationToken cancellationToken = default);
}
