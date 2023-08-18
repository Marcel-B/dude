using DevIt.Eintrag.Adapter.Commands;
using DevIt.Persistence;
using MediatR;

namespace DevIt.Eintrag.Adapter.Handler;

public class UpdateEintragCommandHandler : IRequestHandler<UpdateEintragCommand, Domain.Eintrag>
{
  private readonly IUnitOfWork _uow;

  public UpdateEintragCommandHandler(IUnitOfWork uow)
  {
    _uow = uow;
  }

  public async Task<Domain.Eintrag> Handle(UpdateEintragCommand request, CancellationToken cancellationToken)
  {
    var oldEintrag = await _uow.Eintraege.GetEintragByIdAsync(request.Id, cancellationToken);
    var updateEintragCommand =
      new Domain.Eintrag.UpdateEintrag(request.Text, request.Stunden, request.Datum, request.Abrechenbar);
    var eintrag = Domain.Eintrag.Update(updateEintragCommand, oldEintrag);

    var result = await _uow.Eintraege.UpdateEintragAsync(eintrag, cancellationToken);
    await _uow.CompleteAsync(cancellationToken);
    return result;
  }
}
