using DevIt.Abrechnung.Adapter.Queries;
using DevIt.Repository;
using MediatR;

namespace DevIt.Abrechnung.Adapter.Handler;

public class GetAbrechnungByJahrQueryHandler :
  IRequestHandler<GetAbrechnungByJahrQuery, double>
{
  private readonly IUnitOfWork _unitOfWork;

  public GetAbrechnungByJahrQueryHandler(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  public async Task<double> Handle(GetAbrechnungByJahrQuery request,
    CancellationToken cancellationToken)
  {
    var result = await _unitOfWork.Eintraege.GetEintragByJahrAsync(
      request.Jahr,
      request.Text,
      cancellationToken);
    return result.Select(x => x.Stunden).Sum();
  }
}
