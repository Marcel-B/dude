using com.b_velop.Dude.Bff.UiModel;
using com.b_velop.Dude.Shared;

namespace com.b_velop.Dude.Bff.Services;

public class ProjektService : IProjektService
{
    private readonly Shared.PbiService.PbiServiceClient _client;

    public ProjektService(
        Shared.PbiService.PbiServiceClient client)
    {
        _client = client;
    }

    public async Task<IEnumerable<Projekt>> GetProjekte(
        CancellationToken cancellationToken = default)
    {
        var request = new GetProjekteRequest();
        var reply = await _client.GetProjekteAsync(request, cancellationToken: cancellationToken);
        return reply.Projekte.Select(x => new Projekt(x.Id, x.Name, x.ExterneId));
    }

    public async Task<Projekt> GetProjektById(
        int id,
        CancellationToken cancellationToken = default)
    {
        var request = new GetProjektByIdRequest
        {
            Id = id
        };
        var reply = await _client.GetProjektByIdAsync(request, cancellationToken: cancellationToken);
        return new Projekt(reply.Projekt.Id, reply.Projekt.Name, reply.Projekt.ExterneId);
    }

    public async Task<Projekt> CreateProjekt(
        Projekt projekt,
        CancellationToken cancellationToken = default)
    {
        var request = new CreateProjektRequest
        {
            Name = projekt.Name,
            ExterneId = projekt.ExterneId ?? string.Empty
        };
        var reply = await _client.CreateProjektAsync(request, cancellationToken: cancellationToken);
        return new Projekt(reply.Projekt.Id, reply.Projekt.Name, reply.Projekt.ExterneId);
    }

    public async Task<Projekt> UpdateProjekt(
        Projekt projekt,
        CancellationToken cancellationToken = default)
    {
        var request = new UpdateProjektRequest
        {
            Id = projekt.Id,
            Name = projekt.Name,
            ExterneId = projekt.ExterneId
        };
        var reply = await _client.UpdateProjektAsync(request, cancellationToken: cancellationToken);
        return new Projekt(reply.Projekt.Id, reply.Projekt.Name, reply.Projekt.ExterneId);
    }

    public async Task DeleteProjekt(
        int id,
        CancellationToken cancellationToken = default)
    {
        var request = new DeleteProjektRequest
        {
            Id = id
        };
        await _client.DeleteProjektAsync(request, cancellationToken: cancellationToken);
    }
}