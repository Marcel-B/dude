using DevIt.Domain;
using DevIt.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevIt.Repository;

public class PbiRepository : IPbiRepository
{
  private readonly ApplicationContext _context;

  public PbiRepository(
    ApplicationContext context)
  {
    _context = context;
  }

  public async Task<IList<Pbi>> GetPbisAsync(CancellationToken cancellationToken)
  {
    return await _context.Pbis.ToListAsync(cancellationToken);
  }

  public async Task<Pbi> GetPbiByIdAsync(int id, CancellationToken cancellationToken)
  {
    var pbi = await _context.Pbis.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    return pbi;
  }

  public async Task<Pbi> CreatePbiAsync(Pbi pbi, CancellationToken cancellationToken)
  {
    _context.Pbis.Add(pbi);
    return pbi;
  }

  public async Task<Pbi> UpdatePbiAsync(Pbi pbi, CancellationToken cancellationToken)
  {
    var pbiToUpdate = await _context.Pbis.FirstAsync(x => x.Id == pbi.Id, cancellationToken);
    _context.Entry(pbiToUpdate).CurrentValues.SetValues(pbi);
    return pbiToUpdate;
  }

  public async Task DeletePbiAsync(int id, CancellationToken cancellationToken)
  {
    var pbiToDelete = await _context.Pbis.FirstAsync(x => x.Id == id, cancellationToken);
    _context.Pbis.Remove(pbiToDelete);
  }
}
