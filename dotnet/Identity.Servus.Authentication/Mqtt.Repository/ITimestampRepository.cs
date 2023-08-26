using com.b_velop.Mqtt.Domain;

namespace com.b_velop.Mqtt.Repository;

public interface ITimestampRepository
{
    Task<Timestamp> InsertAsync(
        Timestamp timestamp,
        CancellationToken cancellationToken = default);

    Task<Timestamp?> GetByDateTimeAsync(
        DateTimeOffset dateTime,
        CancellationToken cancellationToken = default);
}