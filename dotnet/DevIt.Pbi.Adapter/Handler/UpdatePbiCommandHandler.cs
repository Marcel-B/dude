using DevIt.Pbi.Adapter.Commands;
using DevIt.Repository;
using MediatR;

namespace DevIt.Pbi.Adapter.Handler;

public class UpdatePbiCommandHandler : IRequestHandler<UpdatePbiCommand, com.b_velop.DevIt.Domain.Pbi>
{
  private readonly IUnitOfWork _unitOfWork;

  public UpdatePbiCommandHandler(
    IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }

  public async Task<com.b_velop.DevIt.Domain.Pbi> Handle(
    UpdatePbiCommand request,
    CancellationToken cancellationToken)
  {
    var createCommand = new com.b_velop.DevIt.Domain.Pbi.CreatePbi(request.Name, request.ProjektId, request.Id);
    var pbi = com.b_velop.DevIt.Domain.Pbi.Create(createCommand);
    var result = await _unitOfWork.Pbis.UpdatePbiAsync(pbi, cancellationToken);
    await _unitOfWork.CompleteAsync(cancellationToken);
    return result;
  }
}
