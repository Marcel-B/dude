using DevIt.Pbi.Adapter.Commands;
using DevIt.Repository;
using MediatR;

namespace DevIt.Pbi.Adapter.Handler;

public class CreatePbiCommandHandler : IRequestHandler<CreatePbiCommand, Domain.Pbi>
{
  private readonly IPbiRepository _pbiRepository;

  public CreatePbiCommandHandler(IPbiRepository pbiRepository)
  {
    _pbiRepository = pbiRepository;
  }

  public async Task<Domain.Pbi> Handle(
    CreatePbiCommand request,
    CancellationToken cancellationToken)
  {
    var createCommand = new Domain.Pbi.CreatePbi(request.Name, request.ProjektId);
    var pbi = Domain.Pbi.Create(createCommand);
    return await _pbiRepository.CreatePbiAsync(pbi, cancellationToken);
  }
}
