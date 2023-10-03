using b_velop;
using MediatR;

namespace DevIt.Moco.Adapter.Queries;

public class GetActivitiesQuery : IRequest<IList<Activity>>
{
  public DateTimeOffset? From { get; set; }
  public DateTimeOffset? To { get; set; }
  public IList<string> ProjectIds { get; set; }
}
