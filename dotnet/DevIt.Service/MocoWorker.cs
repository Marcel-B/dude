using DevIt.Application;
using DevIt.Moco.Adapter.Commands;
using MediatR;
using Quartz;

namespace com.b_velop.DevIt.Service;

public class MocoWorker : IJob
{
    private readonly IMediator _mediator;
    private readonly MocoConfiguration _mocoConfiguration;

    public MocoWorker(
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
            From = (Monat) _mocoConfiguration.From,
            To = (Monat) _mocoConfiguration.To
        };
        _ = await _mediator.Send(command);
    }
}