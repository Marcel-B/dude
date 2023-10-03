using com.b_velop.Dude.Bff.UiModel;

namespace com.b_velop.Dude.Bff.Services;

public interface IPbiService
{
    public Task<IEnumerable<Pbi>> GetPbis(
        CancellationToken cancellationToken = default);

    public Task<Pbi> GetPbiById(
        int id,
        CancellationToken cancellationToken = default);
    
    public Task<Pbi> CreatePbi(
        Pbi pbi,
        CancellationToken cancellationToken = default);
    
    public Task<Pbi> UpdatePbi(
        Pbi pbi,
        CancellationToken cancellationToken = default);
    
    public Task DeletePbi(
        int id,
        CancellationToken cancellationToken = default);
}