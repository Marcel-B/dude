using Mqtt.Domain;
using Mqtt.Repository;

namespace Mqtt.Persistence;

public class MeasurementRepository : IMeasurementRepository
{
  private readonly ApplicationContext _context;

  public MeasurementRepository(ApplicationContext context)
  {
    _context = context;
  }

  public async Task<Measurement> InsertAsync(Measurement measurement, CancellationToken cancellationToken = default)
  {
    var entity = await _context.Measurements.AddAsync(measurement, cancellationToken);
    await _context.SaveChangesAsync(cancellationToken);
    return entity.Entity;
  }
}
