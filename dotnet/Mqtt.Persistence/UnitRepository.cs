using com.b_velop.Mqtt.Domain;
using com.b_velop.Mqtt.Repository;
using Microsoft.EntityFrameworkCore;

namespace com.b_velop.Mqtt.Persistence;

public class UnitRepository : IUnitRepository
{
    private readonly ApplicationContext _context;

    public UnitRepository(
        ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Unit> GetByNameAsync(
        string name,
        CancellationToken cancellationToken = default)
    {
        return await _context.Units.FirstAsync(x => x.Name == name, cancellationToken);
    }
}