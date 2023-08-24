using Mqtt.Shared;

namespace Mqtt.Measurement.Adapter.Command;

public class CreateMeasurementCommand : ICommand
{
    public CreateMeasurementCommand(
        string? topic,
        string? value)
    {
        Topic = topic ?? throw new ArgumentNullException(nameof(topic));
        Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Topic { get; }
    public string Value { get; }
}