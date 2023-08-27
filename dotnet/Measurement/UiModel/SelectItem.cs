namespace com.b_velop.Measurement.UiModel;

public record Measurement(Guid Id, DateTimeOffset Timestamp, double Value);
public record SelectItem(
    Guid Id,
    string Name);