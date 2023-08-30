using DevIt.Eintrag.Adapter.Commands;
using DevIt.Repository;
using MediatR;

namespace DevIt.Eintrag.Adapter.Handler;

public class UpdateEintragCommandHandler : IRequestHandler<UpdateEintragCommand, com.b_velop.DevIt.Domain.Eintrag>
{
  private readonly IUnitOfWork _uow;

  public UpdateEintragCommandHandler(IUnitOfWork uow)
  {
    _uow = uow;
  }

  public async Task<com.b_velop.DevIt.Domain.Eintrag> Handle(UpdateEintragCommand request, CancellationToken cancellationToken)
  {
    var oldEintrag = await _uow.Eintraege.GetEintragByIdAsync(request.Id, cancellationToken);
    var updateEintragCommand =
      new com.b_velop.DevIt.Domain.Eintrag.UpdateEintrag(request.Text, request.Stunden, request.Datum, request.Abrechenbar);
    var eintrag = com.b_velop.DevIt.Domain.Eintrag.Update(updateEintragCommand, oldEintrag);

    var result = await _uow.Eintraege.UpdateEintragAsync(eintrag, cancellationToken);
    await _uow.CompleteAsync(cancellationToken);
    return result;
  }
}
