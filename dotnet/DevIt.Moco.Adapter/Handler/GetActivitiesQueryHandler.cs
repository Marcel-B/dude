using b_velop;
using DevIt.Moco.Adapter.Queries;
using DevIt.Moco.Service;
using MediatR;

namespace DevIt.Moco.Adapter.Handler;

public class GetActivitiesQueryHandler : IRequestHandler<GetActivitiesQuery, IList<Activity>>
{
  private readonly IMocoService _mocoService;

  public GetActivitiesQueryHandler(IMocoService mocoService)
  {
    _mocoService = mocoService ?? throw new ArgumentNullException(nameof(mocoService));
  }

  public async Task<IList<Activity>> Handle(GetActivitiesQuery request, CancellationToken cancellationToken)
  {
    var activities =
      await _mocoService.GetActivitiesAsync(
        request.ProjectIds,
        request.From,
        request.To,
        cancellationToken);
    return activities.ToList();
  }
}
