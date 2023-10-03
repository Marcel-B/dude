namespace com.b_velop.Dude.Bff.UiModel;

public record DeviceReadModel(
    Guid Id,
    string Name,
    IEnumerable<Guid> Sensors);