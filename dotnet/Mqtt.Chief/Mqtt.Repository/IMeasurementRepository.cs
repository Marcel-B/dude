using System.Diagnostics.Metrics;
using com.b_velop.Mqtt.Domain;

namespace com.b_velop.Mqtt.Repository;

public interface IMeasurementRepository
{
    Task<Measurement> InsertAsync(
        Measurement measurement,
        CancellationToken cancellationToken = default);
    
    Task<Measurement> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken = default);
}