using com.b_velop.Mqtt.Domain;

namespace com.b_velop.Mqtt.Repository;

public interface ICoordinateRepository
{
    Task<Coordinate> InsertAsync(
        Coordinate coordinate,
        CancellationToken cancellationToken = default);

    Task<IList<Coordinate>> GetAllAsync(
        CancellationToken cancellationToken = default);
}