using DevIt.Pbi.Adapter.Queries;
using DevIt.Repository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevIt.Pbi.Adapter.Handler;

public class GetPbisQueryHandler : IRequestHandler<GetPbisQuery, IList<com.b_velop.DevIt.Domain.Pbi>>
{
  private readonly IUnitOfWork _unitOfWork;

  public GetPbisQueryHandler(
    IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  public async Task<IList<com.b_velop.DevIt.Domain.Pbi>> Handle(
    GetPbisQuery request,
    CancellationToken cancellationToken)
  {
    var pbis = await _unitOfWork.Pbis.GetPbisAsync(cancellationToken);
    await _unitOfWork.CompleteAsync(cancellationToken);
    return pbis;
  }
}
