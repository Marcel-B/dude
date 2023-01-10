using DevIt.Abrechnung.Adapter.Queries;
using DevIt.Persistence;
using MediatR;

namespace DevIt.Abrechnung.Adapter.Handler;

public class GetAbrechnungByKalenderwocheQueryHandler :
  IRequestHandler<GetAbrechnungByKalenderwocheQuery, double>
{
  private readonly IUnitOfWork _unitOfWork;

  public GetAbrechnungByKalenderwocheQueryHandler(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  public async Task<double> Handle(GetAbrechnungByKalenderwocheQuery request,
    CancellationToken cancellationToken)
  {
    var result = await _unitOfWork.Eintraege.GetEintragByKalenderwocheAsync(
      request.Kalenderwoche,
      request.Jahr,
      request.Text,
      cancellationToken);
    return result.Select(x => x.Stunden).Sum();
  }
}
