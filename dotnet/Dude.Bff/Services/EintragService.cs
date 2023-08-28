using com.b_velop.Dude.Shared;

namespace com.b_velop.Dude.Bff.Services;

public interface IEintragService
{
    Task<IEnumerable<Eintrag>> GetEintraege(
        CancellationToken cancellationToken = default);
}

public class EintragService : IEintragService
{
    private readonly Dude.Shared.EintragService.EintragServiceClient _client;

    public EintragService(
        Dude.Shared.EintragService.EintragServiceClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<Eintrag>> GetEintraege(
        CancellationToken cancellationToken = default)
    {
        var request = new GetEintraegeRequest();
        var reply = await _client.GetEintraegeAsync(request, cancellationToken: cancellationToken);
        return reply.Eintraege.Select(x => x.ToSystem());
    }
}