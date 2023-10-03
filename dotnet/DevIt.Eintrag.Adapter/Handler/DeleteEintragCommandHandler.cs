using DevIt.Eintrag.Adapter.Commands;
using DevIt.Repository;
using MediatR;

namespace DevIt.Eintrag.Adapter.Handler;

public class DeleteEintragCommandHandler : IRequestHandler<DeleteEintragCommand>
{
  private readonly IUnitOfWork _uow;

  public DeleteEintragCommandHandler(IUnitOfWork uow)
  {
    _uow = uow;
  }

  public async Task<Unit> Handle(DeleteEintragCommand request, CancellationToken cancellationToken)
  {
    await _uow.Eintraege.DeleteEintragAsync(request.Id, cancellationToken);
    await _uow.CompleteAsync(cancellationToken);
    return Unit.Value;
  }
}
