using DevIt.Pbi.Adapter.Commands;
using DevIt.Repository;
using MediatR;

namespace DevIt.Pbi.Adapter.Handler;

public class CreatePbiCommandHandler : IRequestHandler<CreatePbiCommand, com.b_velop.DevIt.Domain.Pbi>
{
  private readonly IUnitOfWork _unitOfWork;

  public CreatePbiCommandHandler(
    IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  public async Task<com.b_velop.DevIt.Domain.Pbi> Handle(
    CreatePbiCommand request,
    CancellationToken cancellationToken)
  {
    var createCommand = new com.b_velop.DevIt.Domain.Pbi.CreatePbi(request.Name, request.ProjektId);
    var pbi = com.b_velop.DevIt.Domain.Pbi.Create(createCommand);
    var result = await _unitOfWork.Pbis.CreatePbiAsync(pbi, cancellationToken);
    await _unitOfWork.CompleteAsync(cancellationToken);
    return result;
  }
}
