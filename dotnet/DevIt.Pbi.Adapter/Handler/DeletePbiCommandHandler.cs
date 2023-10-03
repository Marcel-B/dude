using DevIt.Pbi.Adapter.Commands;
using DevIt.Repository;
using MediatR;

namespace DevIt.Pbi.Adapter.Handler;

public class DeletePbiCommandHandler : IRequestHandler<DeletePbiCommand>
{
  private readonly IUnitOfWork _unitOfWork;

  public DeletePbiCommandHandler(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  public async Task<Unit> Handle(DeletePbiCommand request, CancellationToken cancellationToken)
  {
    await _unitOfWork.Pbis.DeletePbiAsync(request.Id, cancellationToken);
    await _unitOfWork.CompleteAsync(cancellationToken);
    return Unit.Value;
  }
}
