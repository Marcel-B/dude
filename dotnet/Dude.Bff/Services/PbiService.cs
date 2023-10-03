using com.b_velop.Dude.Bff.UiModel;
using com.b_velop.Dude.Shared;

namespace com.b_velop.Dude.Bff.Services;

public class PbiService : IPbiService
{
    private readonly Shared.PbiService.PbiServiceClient _client;

    public PbiService(
        Shared.PbiService.PbiServiceClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<Pbi>> GetPbis(
        CancellationToken cancellationToken = default)
    {
        var request = new GetPbisRequest();
        var reply = await _client.GetPbisAsync(request, cancellationToken: cancellationToken);
        return reply.Pbis.Select(x => new Pbi(x.Id, x.Name, x.ProjektId));
    }

    public async Task<Pbi> GetPbiById(
        int id,
        CancellationToken cancellationToken = default)
    {
        var request = new GetPbiByIdRequest
        {
            Id = id
        };
        var reply = await _client.GetPbiByIdAsync(request, cancellationToken: cancellationToken);
        return new Pbi(reply.Pbi.Id, reply.Pbi.Name, reply.Pbi.ProjektId);
    }

    public async Task<Pbi> CreatePbi(
        Pbi pbi,
        CancellationToken cancellationToken = default)
    {
        var request = new CreatePbiRequest
        {
            Name = pbi.Name,
            ProjektId = pbi.ProjektId
        };
        var reply = await _client.CreatePbiAsync(request, cancellationToken: cancellationToken);
        return new Pbi(reply.Pbi.Id, reply.Pbi.Name, reply.Pbi.ProjektId);
    }

    public async Task<Pbi> UpdatePbi(
        Pbi pbi,
        CancellationToken cancellationToken = default)
    {
        var request = new UpdatePbiRequest
        {
            Id = pbi.Id,
            Name = pbi.Name,
            ProjektId = pbi.ProjektId
        };
        var reply = await _client.UpdatePbiAsync(request, cancellationToken: cancellationToken);
        return new Pbi(reply.Pbi.Id, reply.Pbi.Name, reply.Pbi.ProjektId);
    }

    public async Task DeletePbi(
        int id,
        CancellationToken cancellationToken = default)
    {
        var request = new DeletePbiRequest
        {
            Id = id
        };
        await _client.DeletePbiAsync(request, cancellationToken: cancellationToken);
    }
}