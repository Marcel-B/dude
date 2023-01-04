using DevIt.Pbi.Adapter.Commands;
using DevIt.Repository;
using MediatR;

namespace DevIt.Pbi.Adapter.Handler;

public class DeletePbiCommandHandler : IRequestHandler<DeletePbiCommand>
{
  private readonly IPbiRepository _pbiRepository;

  public DeletePbiCommandHandler(IPbiRepository pbiRepository)
  {
    _pbiRepository = pbiRepository;
  }

  public async Task<Unit> Handle(DeletePbiCommand request, CancellationToken cancellationToken)
  {
    await _pbiRepository.DeletePbiAsync(request.Id, cancellationToken);
    return Unit.Value;
  }
}
