using com.b_velop.DevIt.Domain;

namespace DevIt.Repository;

public interface IRepository<T>
{
  IQueryable<T> Query();
}



public interface IEintragRepository : IRepository<Eintrag>
{
  Task<IList<Eintrag>> GetEintraegeAsync(CancellationToken cancellationToken);
  Task<Eintrag> GetEintragByIdAsync(int id, CancellationToken cancellationToken);
  Task<Eintrag> CreateEintragAsync(Eintrag eintrag, CancellationToken cancellationToken);
  Task CreateEintraegeAsync(IEnumerable<Eintrag> eintraege, CancellationToken cancellationToken);
  Task<Eintrag> UpdateEintragAsync(Eintrag eintrag, CancellationToken cancellationToken);
  Task UpdateEintraegeAsync(IList<Eintrag> eintraege, CancellationToken cancellationToken);

  Task<IList<Eintrag>> GetEintragByKalenderwocheAsync(
    int kalenderwoche,
    int jahr,
    string text,
    CancellationToken cancellationToken);

  Task<IList<Eintrag>> GetEintragByMonatAsync(int monat, int jahr, string text, CancellationToken cancellationToken);
  Task<IList<Eintrag>> GetEintragByJahrAsync(int jahr, string text, CancellationToken cancellationToken);
  Task<IList<string>> GetProjekteAsync(CancellationToken cancellationToken);
  Task DeleteEintragAsync(int id, CancellationToken cancellationToken);
}
