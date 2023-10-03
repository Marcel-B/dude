using com.b_velop.DevIt.Domain;
using com.b_velop.Dude.Shared;
using DevIt.Pbi.Adapter.Commands;
using DevIt.Pbi.Adapter.Queries;
using DevIt.Projekt.Adapter.Commands;
using DevIt.Projekt.Adapter.Queries;
using Grpc.Core;
using MediatR;

namespace com.b_velop.DevIt.Service.Services;

public static class ProjektMapperExtensions
{
    public static Projekt ToSystem(
        this ProjektDto dto)
    {
        var cmd = new CreateProjekt(dto.Name, dto.ExterneId);
        return Projekt.Create(cmd);
    }

    public static ProjektDto ToProto(
        this Projekt projekt)
    {
        return new ProjektDto
        {
            Id = projekt.Id,
            Name = projekt.Name,
            ExterneId = projekt.ExterneId ?? ""
        };
    }
}

public static class PbiMapperExtensions
{
    public static Pbi ToSystem(
        this PbiDto dto)
    {
        var cmd = new Pbi.CreatePbi(dto.Name, dto.ProjektId, dto.Id);
        return Pbi.Create(cmd);
    }

    public static PbiDto ToProto(
        this Pbi pbi)
    {
        return new PbiDto
        {
            Id = pbi.Id,
            Name = pbi.Name,
            ProjektId = pbi.ProjektId,
        };
    }
}

public class PbiService : Dude.Shared.PbiService.PbiServiceBase
{
    private readonly IMediator _mediator;

    public PbiService(
        IMediator mediator)
    {
        _mediator = mediator;
    }

    public override async Task<GetProjekteReply> GetProjekte(
        GetProjekteRequest request,
        ServerCallContext context)
    {
        var query = new GetProjekteQuery();
        var result = await _mediator.Send(query, context.CancellationToken);
        return new GetProjekteReply
        {
            Projekte = {result.Select(x => x.ToProto())}
        };
    }

    public override async Task<CreateProjektReply> CreateProjekt(
        CreateProjektRequest request,
        ServerCallContext context)
    {
        var cmd = new CreateProjektCommand(request.Name, request.ExterneId ?? string.Empty);
        var result = await _mediator.Send(cmd, context.CancellationToken);
        return new CreateProjektReply
        {
            Projekt = result.ToProto()
        };
    }

    public override async Task<UpdateProjektReply> UpdateProjekt(
        UpdateProjektRequest request,
        ServerCallContext context)
    {
        var cmd = new UpdateProjektCommand(request.Id, request.Name);
        var result = await _mediator.Send(cmd, context.CancellationToken);
        return new UpdateProjektReply
        {
            Projekt = result.ToProto()
        };
    }

    public override async Task<DeleteProjektReply> DeleteProjekt(
        DeleteProjektRequest request,
        ServerCallContext context)
    {
        var cmd = new DeleteProjektCommand(request.Id);
        await _mediator.Send(cmd, context.CancellationToken);
        return new DeleteProjektReply();
    }

    public override async Task<GetPbisReply> GetPbis(
        GetPbisRequest request,
        ServerCallContext context)
    {
        var query = new GetPbisQuery();
        var result = await _mediator.Send(query, context.CancellationToken);
        return new GetPbisReply
        {
            Pbis = {result.Select(x => x.ToProto())}
        };
    }

    public override async Task<GetPbiByIdReply> GetPbiById(
        GetPbiByIdRequest request,
        ServerCallContext context)
    {
        var query = new GetPbiByIdQuery(request.Id);
        var result = await _mediator.Send(query, context.CancellationToken);
        return new GetPbiByIdReply
        {
            Pbi = result.ToProto()
        };
    }

    public override async Task<CreatePbiReply> CreatePbi(
        CreatePbiRequest request,
        ServerCallContext context)
    {
        var cmd = new CreatePbiCommand(request.Name, request.ProjektId);
        var result = await _mediator.Send(cmd, context.CancellationToken);
        return new CreatePbiReply
        {
            Pbi = result.ToProto()
        };
    }

    public override async Task<UpdatePbiReply> UpdatePbi(
        UpdatePbiRequest request,
        ServerCallContext context)
    {
        var cmd = new UpdatePbiCommand(request.Id, request.Name, request.ProjektId);
        var result = await _mediator.Send(cmd, context.CancellationToken);
        return new UpdatePbiReply
        {
            Pbi = result.ToProto()
        };
    }

    public override async Task<DeletePbiReply> DeletePbi(
        DeletePbiRequest request,
        ServerCallContext context)
    {
        var cmd = new DeletePbiCommand(request.Id);
        await _mediator.Send(cmd, context.CancellationToken);
        return new DeletePbiReply();
    }
}