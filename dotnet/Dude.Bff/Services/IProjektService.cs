using com.b_velop.Dude.Bff.UiModel;

namespace com.b_velop.Dude.Bff.Services;

public interface IProjektService
{
    public Task<IEnumerable<Projekt>> GetProjekte(
        CancellationToken cancellationToken = default);

    public Task<Projekt> GetProjektById(
        int id,
        CancellationToken cancellationToken = default);

    public Task<Projekt> CreateProjekt(
        Projekt projekt,
        CancellationToken cancellationToken = default);

    public Task<Projekt> UpdateProjekt(
        Projekt projekt,
        CancellationToken cancellationToken = default);

    public Task DeleteProjekt(
        int id,
        CancellationToken cancellationToken = default);
}