using System.Collections.Immutable;
using DevIt.Domain;
using DevIt.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevIt.Repository;

public class ProjektRepository : IProjektRepository
{
  private readonly ApplicationContext _context;

  public ProjektRepository(ApplicationContext context)
  {
    _context = context;
  }

  public async Task<Projekt> CreateProjektAsync(Projekt projekt, CancellationToken cancellationTokens)
  {
    _context.Projekte.Add(projekt);
    await _context.SaveChangesAsync(cancellationTokens);
    return projekt;
  }

  public async Task<ICollection<Projekt>> GetProjekteAsync(CancellationToken cancellationToken)
  {
    return _context.Projekte.ToImmutableList();
  }

  public async Task<Projekt> GetProjektByIdAsync(string id, CancellationToken cancellationToken)
  {
    return await _context.Projekte.FirstAsync(projekt => projekt.Id == id, cancellationToken);
  }

  public async Task DeleteProjektAsync(string id, CancellationToken cancellationToken)
  {
    var projekt = await _context.Projekte.FirstAsync(projekt => projekt.Id == id, cancellationToken);
    _context.Projekte.Remove(projekt);
    await _context.SaveChangesAsync(cancellationToken);
  }

  public async Task UpdateProjektAsync(Projekt projekt, CancellationToken cancellationToken)
  {
    var projektToUpdate = await _context.Projekte.FirstAsync(pr => pr.Id == projekt.Id, cancellationToken);
    _context.Entry(projektToUpdate).CurrentValues.SetValues(projekt);
    await _context.SaveChangesAsync(cancellationToken);
  }
}
