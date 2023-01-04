using DevIt.Domain;

namespace DevIt.Repository;

public interface IPbiRepository
{
  Task<IList<Pbi>> GetPbisAsync(CancellationToken cancellationToken);
  Task<Pbi> GetPbiByIdAsync(int id, CancellationToken cancellationToken);
  Task<Pbi> CreatePbiAsync(Pbi pbi, CancellationToken cancellationToken);
  Task<Pbi> UpdatePbiAsync(int id, Pbi pbi, CancellationToken cancellationToken);
  Task DeletePbiAsync(int id, CancellationToken cancellationToken);
}
