using com.b_velop.DevIt.Domain;

namespace DevIt.Repository;

public interface IPbiRepository
{
  Task<IList<Pbi>> GetPbisAsync(CancellationToken cancellationToken);
  Task<Pbi> GetPbiByIdAsync(int id, CancellationToken cancellationToken);
  Task<Pbi> CreatePbiAsync(Pbi pbi, CancellationToken cancellationToken);
  Task<Pbi> UpdatePbiAsync(Pbi pbi, CancellationToken cancellationToken);
  Task DeletePbiAsync(int id, CancellationToken cancellationToken);
}
