using DevIt.Application;
using DevIt.Moco.Adapter.Commands;
using MediatR;
using Quartz;

namespace com.b_velop.DevIt.Service;

public class GetFoo : IJob
{
    private readonly IMediator _mediator;
    private readonly MocoConfiguration _mocoConfiguration;

    public GetFoo(
        IMediator mediator,
        MocoConfiguration mocoConfiguration)
    {
        _mediator = mediator;
        _mocoConfiguration = mocoConfiguration;
    }

    public async Task Execute(
        IJobExecutionContext context)
    {
        var command = new CreateEintraegeByMocoCommand
        {
            ProjektIds = _mocoConfiguration.Projekte,
            Monat = (Monat) DateTimeOffset.Now.Month
        };
        _ = await _mediator.Send(command);
    }
}