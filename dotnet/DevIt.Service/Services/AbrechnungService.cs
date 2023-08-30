using com.b_velop.Dude.Shared;
using DevIt.Abrechnung.Adapter.Queries;
using Grpc.Core;
using MediatR;

namespace com.b_velop.DevIt.Service.Services;

public class AbrechnungService : Dude.Shared.AbrechnungService.AbrechnungServiceBase
{
    private readonly IMediator _mediator;

    public AbrechnungService(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<GetByKalenderwocheReply> GetByKalenderwoche(
        GetByKalenderwocheRequest request,
        ServerCallContext context)
    {
        var result =
            await _mediator.Send(
                new GetAbrechnungByKalenderwocheQuery(request.Kalenderwoche, request.Jahr, request.Text),
                context.CancellationToken);
        return new GetByKalenderwocheReply
        {
            Stunden = result
        };
    }

    public override async Task<GetByMonatReply> GetByMonat(
        GetByMonatRequest request,
        ServerCallContext context)
    {
        var result = await _mediator.Send(new GetAbrechnungByMonatQuery(request.Monat, request.Jahr, request.Text),
            context.CancellationToken);
        return new GetByMonatReply
        {
            Stunden = result,
        };
    }

    public override async Task<GetByJahrReply> GetByJahr(
        GetByJahrRequest request,
        ServerCallContext context)
    {
        var result = await _mediator.Send(new GetAbrechnungByJahrQuery(request.Jahr, request.Text),
            context.CancellationToken);
        return new GetByJahrReply
        {
            Stunden = result
        };
    }

    public override async Task<GetProjekteReply> GetProjekte(
        GetProjekteRequest request,
        ServerCallContext context)
    {
        var result = await _mediator.Send(new GetProjekteQuery(), context.CancellationToken);
        return new GetProjekteReply
        {
            Projekte = {result}
        };
    }
}