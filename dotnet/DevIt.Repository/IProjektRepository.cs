using com.b_velop.DevIt.Domain;

namespace DevIt.Repository;

public interface IProjektRepository
{
  Task<Projekt> CreateProjektAsync(Projekt projekt, CancellationToken cancellationToken);
  Task<ICollection<Projekt>> GetProjekteAsync(CancellationToken cancellationToken);
  Task<Projekt> GetProjektByIdAsync(string id, CancellationToken cancellationToken);
  Task DeleteProjektAsync(string id, CancellationToken cancellationToken);
  Task<Projekt> UpdateProjektAsync(Projekt projekt, CancellationToken cancellationToken);
}
