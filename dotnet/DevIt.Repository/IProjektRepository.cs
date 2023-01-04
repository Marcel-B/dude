using DevIt.Domain;

namespace DevIt.Repository;

public interface IProjektRepository
{
  Task<Projekt> CreateProjektAsync(Projekt projekt, CancellationToken cancellationToken);
  Task<ICollection<Projekt>> GetProjekteAsync(CancellationToken cancellationToken);
  Task<Projekt> GetProjektByIdAsync(string id, CancellationToken cancellationToken);
  Task DeleteProjektAsync(string id, CancellationToken cancellationToken);
  Task UpdateProjektAsync(Projekt projekt, CancellationToken cancellationToken);
}
