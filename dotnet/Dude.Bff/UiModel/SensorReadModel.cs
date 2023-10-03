namespace com.b_velop.Dude.Bff.UiModel;

public record SensorReadModel(
    Guid Id,
    string Name,
    string Unit,
    Guid DeviceId);