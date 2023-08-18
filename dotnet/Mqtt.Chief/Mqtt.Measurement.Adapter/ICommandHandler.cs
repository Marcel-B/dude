namespace Mqtt.Measurement.Adapter;

public interface ICommandHandler<in TCommand> where TCommand : ICommand
{
  Task HandleAsync(TCommand command, CancellationToken cancellationToken = default);
}
