namespace Mqtt.Measurement.Adapter;

public class CreateMeasurementCommand : ICommand
{
  public string Topic { get; }
  public string Value { get; }

  public CreateMeasurementCommand(string? topic, string? value)
  {
    Topic = topic ?? throw new ArgumentNullException(nameof(topic));
    Value = value ?? throw new ArgumentNullException(nameof(value));
  }
}
