using com.b_velop.Mqtt.Domain;
using com.b_velop.Mqtt.Repository;
using Microsoft.EntityFrameworkCore;

namespace com.b_velop.Mqtt.Persistence;

public class MeasurementRepository : IMeasurementRepository
{
    private readonly ApplicationContext _context;

    public MeasurementRepository(
        ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Measurement> InsertAsync(
        Measurement measurement,
        CancellationToken cancellationToken = default)
    {
        var entity = await _context.Measurements.AddAsync(measurement, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Entity;
    }

    public async Task<Measurement> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context.Measurements.FirstAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Measurement>> GetBySensorIdAsync(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        return await _context
            .Measurements.Where(x => x.SensorId == id)
            .ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<Measurement>> GetBySensorIdAsync(
        Guid id,
        DateTimeOffset? from = null,
        DateTimeOffset? to = null,
        CancellationToken cancellationToken = default)
    {
        var query = _context
            .Measurements
            .Include(x => x.Timestamp)
            .Where(x => x.SensorId == id);
        if (from.HasValue)
            query = query.Where(x => x.Timestamp.DateTime >= from.Value);
        if (to.HasValue)
            query = query.Where(x => x.Timestamp.DateTime <= to.Value);
        return await query.OrderBy(x => x.Timestamp.DateTime).ToListAsync(cancellationToken);
    }
}