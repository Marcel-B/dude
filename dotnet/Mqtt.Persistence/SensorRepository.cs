using com.b_velop.Mqtt.Domain;
using com.b_velop.Mqtt.Repository;
using Microsoft.EntityFrameworkCore;

namespace com.b_velop.Mqtt.Persistence;

public class SensorRepository : ISensorRepository
{
    private readonly ApplicationContext _context;

    public SensorRepository(
        ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Sensor?> GetSensorAsync(
        string name,
        Guid deviceId,
        Guid unitId,
        CancellationToken cancellationToken = default)
    {
        return await _context.Sensors.FirstOrDefaultAsync(
            x => x.Name == name && x.DeviceId == deviceId && x.UnitId == unitId, cancellationToken);
    }

    public async Task<ICollection<Sensor>> GetSensorsAsync(
        CancellationToken cancellationToken = default)
    {
        var sensors = await _context
            .Sensors
            .Include(x => x.Unit)
            .Include(x => x.Device)
            .ToListAsync(cancellationToken);
        return sensors;
    }

    public async Task<Sensor> InsertAsync(
        Sensor sensor,
        CancellationToken cancellationToken = default)
    {
        var entity = await _context.Sensors.AddAsync(sensor, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Entity;
    }
}