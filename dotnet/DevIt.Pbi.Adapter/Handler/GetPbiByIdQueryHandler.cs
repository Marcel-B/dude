using DevIt.Pbi.Adapter.Queries;
using DevIt.Persistence;
using MediatR;

namespace DevIt.Pbi.Adapter.Handler;

public class GetPbiByIdQueryHandler : IRequestHandler<GetPbiByIdQuery, Domain.Pbi>
{
  private readonly IUnitOfWork _unitOfWork;

  public GetPbiByIdQueryHandler(
    IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  public async Task<Domain.Pbi> Handle(
    GetPbiByIdQuery request,
    CancellationToken cancellationToken)
  {
    var pbi = await _unitOfWork.Pbis.GetPbiByIdAsync(request.Id, cancellationToken);
    await _unitOfWork.CompleteAsync(cancellationToken);
    return pbi;
  }
}
