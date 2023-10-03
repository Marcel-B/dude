namespace com.b_velop.Dude.Bff.UiModel;

public record CoordinateDto
{
    public DateTimeOffset Time { get; init; }
    public double Latitude { get; init; }
    public double Longitude { get; init; }
}