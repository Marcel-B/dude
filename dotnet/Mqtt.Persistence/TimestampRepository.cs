using com.b_velop.Mqtt.Domain;
using com.b_velop.Mqtt.Repository;
using Microsoft.EntityFrameworkCore;

namespace com.b_velop.Mqtt.Persistence;

public class TimestampRepository : ITimestampRepository
{
    private readonly ApplicationContext _context;

    public TimestampRepository(
        ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Timestamp?> GetByDateTimeAsync(
        DateTimeOffset DateTime,
        CancellationToken cancellationToken = default)
    {
        return await _context.Timestamps.FirstOrDefaultAsync(x => x.DateTime == DateTime, cancellationToken);
    }

    public async Task<Timestamp> InsertAsync(
        Timestamp timestamp,
        CancellationToken cancellationToken = default)
    {
        var entity = await _context.Timestamps.AddAsync(timestamp, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Entity;
    }
}