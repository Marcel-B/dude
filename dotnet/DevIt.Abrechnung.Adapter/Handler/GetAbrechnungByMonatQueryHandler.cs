using DevIt.Abrechnung.Adapter.Queries;
using DevIt.Persistence;
using MediatR;

namespace DevIt.Abrechnung.Adapter.Handler;

public class GetAbrechnungByMonatQueryHandler :
  IRequestHandler<GetAbrechnungByMonatQuery, double>
{
  private readonly IUnitOfWork _unitOfWork;

  public GetAbrechnungByMonatQueryHandler(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  public async Task<double> Handle(GetAbrechnungByMonatQuery request,
    CancellationToken cancellationToken)
  {
    var result = await _unitOfWork.Eintraege.GetEintragByMonatAsync(
      request.Monat,
      request.Jahr,
      request.Text,
      cancellationToken);
    return result.Select(x => x.Stunden).Sum();
  }
}
