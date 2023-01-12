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

public class GetProjekteQueryHandler :
  IRequestHandler<GetProjekteQuery, IEnumerable<string>>
{
  private readonly IUnitOfWork _unitOfWork;

  public GetProjekteQueryHandler(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  public async Task<IEnumerable<string>> Handle(GetProjekteQuery request,
    CancellationToken cancellationToken)
  {
    var result = await _unitOfWork.Eintraege.GetProjekteAsync(cancellationToken);
    return result;
  }
}
