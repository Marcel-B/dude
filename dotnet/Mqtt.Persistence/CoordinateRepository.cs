using com.b_velop.Mqtt.Domain;
using com.b_velop.Mqtt.Repository;
using Microsoft.EntityFrameworkCore;

namespace com.b_velop.Mqtt.Persistence;

public class CoordinateRepository : ICoordinateRepository
{
    private readonly ApplicationContext _context;

    public CoordinateRepository(
        ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Coordinate> InsertAsync(
        Coordinate coordinate,
        CancellationToken cancellationToken = default)
    {
        var entity = await _context.Coordinates.AddAsync(coordinate, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return entity.Entity;
    }

    public async Task<IList<Coordinate>> GetAllAsync(
        CancellationToken cancellationToken = default)
    {
        return await _context.Coordinates.ToListAsync(cancellationToken);
    }
}