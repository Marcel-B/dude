using System.Globalization;
using DevIt.Domain;
using DevIt.Persistence;
using Microsoft.EntityFrameworkCore;

namespace DevIt.Repository;

internal static class EintragRepositoryExtensions
{
  static CultureInfo myCI = new CultureInfo("de-DE");
  static Calendar myCal = myCI.Calendar;
  static CalendarWeekRule myCWR = myCI.DateTimeFormat.CalendarWeekRule;
  static DayOfWeek myFirstDOW = myCI.DateTimeFormat.FirstDayOfWeek;

  internal static IQueryable<Eintrag> ByText(this IQueryable<Eintrag> eintraege, string text)
    => eintraege.Where(e => e.Text == text);

  internal static async Task<IList<Eintrag>> ByKalenderwoche(this IQueryable<Eintrag> eintraege,
    int kalenderwoche,
    CancellationToken cancellationToken)
  {
    var result = await eintraege.ToListAsync(cancellationToken);
    return result.Where(x => myCal.GetWeekOfYear(x.Datum.Date, myCWR, myFirstDOW) == kalenderwoche).ToList();
  }

  internal static IQueryable<Eintrag> ByMonat(this IQueryable<Eintrag> eintraege, int monat)
    => eintraege.Where(x => x.Datum.Month == monat);

  internal static IQueryable<Eintrag> ByJahr(this IQueryable<Eintrag> eintraege, int jahr)
    => eintraege.Where(x => x.Datum.Year == jahr);
}

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

  private IQueryable<Eintrag> ByText(string text)
  {
    return _context.Eintraege.Where(x => x.Text.Contains(text));
  }

  public async Task<IList<Eintrag>> GetEintragByKalenderwocheAsync(
    int kalenderwoche,
    int jahr,
    string name,
    CancellationToken cancellationToken)
  {
    return await _context
      .Eintraege
      .ByText(name)
      .ByJahr(jahr)
      .ByKalenderwoche(kalenderwoche, cancellationToken);
  }

  public async Task<IList<Eintrag>> GetEintragByMonatAsync(int monat, int jahr, string text,
    CancellationToken cancellationToken)
  {
    return await _context.Eintraege
      .ByText(text)
      .ByJahr(jahr)
      .ByMonat(monat)
      .ToListAsync(cancellationToken);
  }

  public async Task<IList<Eintrag>> GetEintragByJahrAsync(int jahr, string text, CancellationToken cancellationToken)
  {
    return await _context.Eintraege
      .ByText(text)
      .ByJahr(jahr)
      .ToListAsync(cancellationToken);
  }

  public async Task DeleteEintragAsync(int id, CancellationToken cancellationToken)
  {
    var eintragToDelte = await _context.Eintraege.FirstAsync(x => x.Id == id, cancellationToken);
    _context.Eintraege.Remove(eintragToDelte);
  }
}
