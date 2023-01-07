using DevIt.Eintrag.Adapter.Commands;
using DevIt.Persistence;
using MediatR;

namespace DevIt.Eintrag.Adapter.Handler;

public class CreateEintragCommandHandler : IRequestHandler<CreateEintragCommand, Domain.Eintrag>
{
  private readonly IUnitOfWork _uow;

  public CreateEintragCommandHandler(IUnitOfWork uow)
  {
    _uow = uow;
  }

  public async Task<Domain.Eintrag> Handle(CreateEintragCommand request, CancellationToken cancellationToken)
  {
    var createEintragCommand =
      new Domain.Eintrag.CreateEintrag(request.Text, request.Stunden, request.Datum, request.Abrechenbar);
    var eintrag = Domain.Eintrag.Create(createEintragCommand);
    var result = await _uow.Eintraege.CreateEintragAsync(eintrag, cancellationToken);
    await _uow.CompleteAsync(cancellationToken);
    return result;
  }
}
