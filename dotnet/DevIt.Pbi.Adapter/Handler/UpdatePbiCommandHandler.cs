using DevIt.Pbi.Adapter.Commands;
using DevIt.Persistence;
using MediatR;

namespace DevIt.Pbi.Adapter.Handler;

public class UpdatePbiCommandHandler : IRequestHandler<UpdatePbiCommand, Domain.Pbi>
{
  private readonly IUnitOfWork _unitOfWork;

  public UpdatePbiCommandHandler(
    IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  public async Task<Domain.Pbi> Handle(
    UpdatePbiCommand request,
    CancellationToken cancellationToken)
  {
    var createCommand = new Domain.Pbi.CreatePbi(request.Name, request.ProjektId, request.Id);
    var pbi = Domain.Pbi.Create(createCommand);
    var result = await _unitOfWork.Pbis.UpdatePbiAsync(pbi, cancellationToken);
    await _unitOfWork.CompleteAsync(cancellationToken);
    return result;
  }
}
