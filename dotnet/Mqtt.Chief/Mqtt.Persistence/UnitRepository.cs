using Microsoft.EntityFrameworkCore;
using Mqtt.Domain;
using Mqtt.Repository;

namespace Mqtt.Persistence;

public class UnitRepository : IUnitRepository
{
  private readonly ApplicationContext _context;

  public UnitRepository(ApplicationContext context)
  {
    _context = context;
  }

  public async Task<Unit> GetByNameAsync(string name, CancellationToken cancellationToken = default)
  {
    return await _context.Units.FirstAsync(x => x.Name == name, cancellationToken);
  }

}
