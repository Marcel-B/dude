using com.b_velop.Mqtt.Domain;
using com.b_velop.Mqtt.Repository;
using Microsoft.EntityFrameworkCore;

namespace com.b_velop.Mqtt.Persistence;

public class DeviceRepository : IDeviceRepository
{
    private readonly ApplicationContext _context;

    public DeviceRepository(
        ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Device?> GetByNameAsync(
        string name,
        CancellationToken cancellationToken = default)
    {
        return await _context.Devices.FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
    }

    public async Task<Device?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context
            .Devices
            .Include(x => x.Sensors)
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Device>> GetDevicesAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Devices.ToListAsync(cancellationToken);
    }

    public async Task<Device> InsertAsync(
        Device device,
        CancellationToken cancellationToken = default)
    {
        var entity = await _context.Devices.AddAsync(device, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Entity;
    }
}