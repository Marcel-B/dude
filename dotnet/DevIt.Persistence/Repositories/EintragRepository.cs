using DevIt.Domain;
using DevIt.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevIt.Repository;

public class EintragRepository : IEintragRepository
{
  private readonly ApplicationContext _context;

  public EintragRepository(ApplicationContext context)
  {
    _context = context;
  }

  public async Task<IList<Eintrag>> GetEintraegeAsync(CancellationToken cancellationToken)
  {
    return await _context.Eintraege.ToListAsync(cancellationToken);
  }

  public async Task<Eintrag> GetEintragByIdAsync(int id, CancellationToken cancellationToken)
  {
    var eintrag = await _context.Eintraege.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    return eintrag;
  }

  public async Task<Eintrag> CreateEintragAsync(Eintrag eintrag, CancellationToken cancellationToken)
  {
    _context.Eintraege.Add(eintrag);
    return eintrag;
  }

  public async Task<Eintrag> UpdateEintragAsync(Eintrag eintrag, CancellationToken cancellationToken)
  {
    var eintragToUpdate = await _context.Eintraege.FirstAsync(x => x.Id == eintrag.Id, cancellationToken);
    _context.Entry(eintragToUpdate).CurrentValues.SetValues(eintrag);
    return eintrag;
  }

  public async Task DeleteEintragAsync(int id, CancellationToken cancellationToken)
  {
    var eintragToDelte = await _context.Eintraege.FirstAsync(x => x.Id == id, cancellationToken);
    _context.Eintraege.Remove(eintragToDelte);
  }
}
