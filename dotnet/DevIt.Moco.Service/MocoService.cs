using System.Text.Json;
using b_velop;

namespace DevIt.Moco.Service
{
  public class MocoService : IMocoService
  {
    private readonly HttpClient _httpClient;

    public MocoService(HttpClient httpClient)
    {
      _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
    }

    public async Task<IEnumerable<Activity>> GetActivitiesAsync(
      IList<string> projectIds,
      DateTimeOffset? from = null,
      DateTimeOffset? to = null,
      CancellationToken cancellationToken = default)
    {
      var activities = new List<Activity>();
      foreach (var projectId in projectIds)
      {
        var projectActivities = await RequestActivityAsync(
          projectId,
          from,
          to,
          cancellationToken);
        activities.AddRange(projectActivities);
      }

      return activities;
    }

    private async Task<IList<Activity>> RequestActivityAsync(
      string projectId,
      DateTimeOffset? from,
      DateTimeOffset? to,
      CancellationToken cancellationToken)
    {
      var url = BuildActivitiesUrl(projectId, from, to);
      var response = await _httpClient.GetStringAsync(url, cancellationToken);
      return JsonSerializer.Deserialize<Activity[]>(response) ?? Array.Empty<Activity>();
    }

    private string BuildActivitiesUrl(string projectId, DateTimeOffset? from, DateTimeOffset? to)
    {
      var fromString = from?.ToString("yyyy-MM-dd");
      var toString = to?.ToString("yyyy-MM-dd");

      var url = $"/api/v1/activities?fproject_id={projectId}";

      if (!string.IsNullOrEmpty(fromString) && !string.IsNullOrEmpty(toString))
      {
        url += $"&from={fromString}&to={toString}";
      }

      return url;
    }
  }
}
