using com.b_velop.Dude.Shared;
using com.b_velop.Mqtt.Repository;
using Grpc.Core;

namespace com.b_velop.Mqtt.Gps.Service;

public class GpsService : Dude.Shared.Gps.GpsBase
{
    private readonly ICoordinateRepository _coordinateRepository;

    public GpsService(ICoordinateRepository coordinateRepository)
    {
        _coordinateRepository = coordinateRepository;
    }
    
    public override async Task<GetAllCoordinatesReply> GetAllCoordinates(
        GetAllCoordinatesRequest request,
        ServerCallContext context)
    {
        var values = await _coordinateRepository.GetAllAsync(context.CancellationToken);
        var reply = new GetAllCoordinatesReply
        {
            Coordinates = {values.Select(x => x.ToProto())}
        };
        return reply;
    }
}