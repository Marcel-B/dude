using DevIt.Pbi.Adapter.Commands;
using DevIt.Persistence;
using MediatR;

namespace DevIt.Pbi.Adapter.Handler;

public class CreatePbiCommandHandler : IRequestHandler<CreatePbiCommand, Domain.Pbi>
{
  private readonly IUnitOfWork _unitOfWork;

  public CreatePbiCommandHandler(
    IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  public async Task<Domain.Pbi> Handle(
    CreatePbiCommand request,
    CancellationToken cancellationToken)
  {
    var createCommand = new Domain.Pbi.CreatePbi(request.Name, request.ProjektId);
    var pbi = Domain.Pbi.Create(createCommand);
    var result = await _unitOfWork.Pbis.CreatePbiAsync(pbi, cancellationToken);
    await _unitOfWork.CompleteAsync(cancellationToken);
    return result;
  }
}
