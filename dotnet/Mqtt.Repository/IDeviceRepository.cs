using com.b_velop.Mqtt.Domain;

namespace com.b_velop.Mqtt.Repository;

public interface IDeviceRepository
{
    Task<IEnumerable<Device>> GetDevicesAsync(
        CancellationToken cancellationToken = default);

    Task<Device?> GetByNameAsync(
        string name,
        CancellationToken cancellationToken = default);

    Task<Device?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<Device> InsertAsync(
        Device device,
        CancellationToken cancellationToken = default);
}