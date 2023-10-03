using b_velop;

namespace DevIt.Moco.Service;

public interface IMocoService
{
  Task<IEnumerable<Activity>> GetActivitiesAsync(
    IList<string> projectIds,
    DateTimeOffset? from = null,
    DateTimeOffset? to = null,
    CancellationToken cancellationToken = default);
}
