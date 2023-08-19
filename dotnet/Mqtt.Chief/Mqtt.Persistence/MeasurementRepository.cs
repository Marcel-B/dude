using com.b_velop.Mqtt.Domain;
using com.b_velop.Mqtt.Repository;

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
}