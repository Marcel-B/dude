using DevIt.Pbi.Adapter.Commands;
using DevIt.Repository;
using MediatR;

namespace DevIt.Pbi.Adapter.Handler;

public class UpdatePbiCommandHandler : IRequestHandler<UpdatePbiCommand, Domain.Pbi>
{
  private readonly IPbiRepository _pbiRepository;

  public UpdatePbiCommandHandler(IPbiRepository pbiRepository)
  {
    _pbiRepository = pbiRepository;
  }

  public async Task<Domain.Pbi> Handle(
    UpdatePbiCommand request,
    CancellationToken cancellationToken)
  {
    var createCommand = new Domain.Pbi.CreatePbi(request.Name, request.ProjektId);
    var pbi = Domain.Pbi.Create(createCommand);
    return await _pbiRepository.UpdatePbiAsync(request.Id, pbi, cancellationToken);
  }
}
