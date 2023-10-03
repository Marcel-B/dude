using com.b_velop.Dude.Bff.UiModel;
using com.b_velop.Dude.Shared;

namespace com.b_velop.Dude.Bff.Services;

public class GpsService : IGpsService
{
    private readonly Gps.GpsClient _gpsClient;

    public GpsService(
        Gps.GpsClient gpsClient)
    {
        _gpsClient = gpsClient;
    }

    public async Task<IEnumerable<CoordinateDto>> GetCoordinates(
        CancellationToken cancellationToken = default)
    {
        var request = new GetAllCoordinatesRequest();
        var reply = await _gpsClient.GetAllCoordinatesAsync(request, cancellationToken: cancellationToken);
        return reply.Coordinates.Select(x => new CoordinateDto
        {
            Time = x.Timestamp.ToDateTimeOffset()!.Value,
            Latitude = x.Latitude,
            Longitude = x.Longitude
        });
    }
}