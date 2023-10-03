using DevIt.Eintrag.Adapter.Commands;
using DevIt.Repository;
using MediatR;

namespace DevIt.Eintrag.Adapter.Handler;

public class CreateEintragCommandHandler : IRequestHandler<CreateEintragCommand, com.b_velop.DevIt.Domain.Eintrag>
{
  private readonly IUnitOfWork _uow;

  public CreateEintragCommandHandler(IUnitOfWork uow)
  {
    _uow = uow;
  }

  public async Task<com.b_velop.DevIt.Domain.Eintrag> Handle(CreateEintragCommand request, CancellationToken cancellationToken)
  {
    var createEintragCommand =
      new com.b_velop.DevIt.Domain.Eintrag.CreateEintrag(request.Text, request.Stunden, request.Datum, request.Abrechenbar);
    var eintrag = com.b_velop.DevIt.Domain.Eintrag.Create(createEintragCommand);
    var result = await _uow.Eintraege.CreateEintragAsync(eintrag, cancellationToken);
    await _uow.CompleteAsync(cancellationToken);
    return result;
  }
}
