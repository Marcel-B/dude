using com.b_velop.Dude.Shared;
using DevIt.Eintrag.Adapter.Commands;
using DevIt.Eintrag.Adapter.Queries;
using Grpc.Core;
using MediatR;

namespace com.b_velop.DevIt.Service.Services;

public class EintragService : com.b_velop.Dude.Shared.EintragService.EintragServiceBase
{
    private readonly IMediator _mediator;

    public EintragService(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<GetEintraegeReply> GetEintraege(
        GetEintraegeRequest request,
        ServerCallContext context)
    {
        var result = await _mediator.Send(new GetEintraegeQuery(), context.CancellationToken);
        var reply = new GetEintraegeReply
        {
            Eintraege = {result.Select(x => x.ToProto())}
        };
        return reply;
    }

    public override async Task<GetEintragReply> GetEintrag(
        GetEintragRequest request,
        ServerCallContext context)
    {
        var id = request.Id;
        var result = await _mediator.Send(new GetEintragByIdQuery(id), context.CancellationToken);
        return new GetEintragReply
        {
            Eintrag = result.ToProto()
        };
    }

    public override async Task<CreateEintragReply> CreateEintrag(
        CreateEintragRequest request,
        ServerCallContext context)
    {
        var command = new CreateEintragCommand(
            request.Eintrag.Text,
            request.Eintrag.Datum.ToDateTimeOffset()!
                .Value,
            request.Eintrag.Stunden,
            request.Eintrag.Abrechenbar);
        var result = await _mediator.Send(command, context.CancellationToken);
        return new CreateEintragReply
        {
            Eintrag = result.ToProto()
        };
    }

    public override async Task<UpdateEintragReply> UpdateEintrag(
        UpdateEintragRequest request,
        ServerCallContext context)
    {
        var command = new UpdateEintragCommand(
            request.Eintrag.Id,
            request.Eintrag.Text,
            request.Eintrag.Datum.ToDateTimeOffset()!
                .Value,
            request.Eintrag.Stunden,
            request.Eintrag.Abrechenbar);
        var result = await _mediator.Send(command, context.CancellationToken);
        return new UpdateEintragReply
        {
            Eintrag = result.ToProto()
        };
    }

    public override async Task<DeleteEintragReply> DeleteEintrag(
        DeleteEintragRequest request,
        ServerCallContext context)
    {
        await _mediator.Send(new DeleteEintragCommand(request.Id), context.CancellationToken);
        return new DeleteEintragReply();
    }
}