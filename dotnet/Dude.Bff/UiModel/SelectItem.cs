namespace com.b_velop.Dude.Bff.UiModel;

public record Measurement(Guid Id, DateTimeOffset Timestamp, double Value);
public record SelectItem(
    Guid Id,
    string Name);