using DevIt.Domain;
using DevIt.Projekt.Adapter.Command;
using DevIt.Repository;
using MediatR;

namespace DevIt.Projekt.Adapter.Handler;

public class UpdateProjektCommandHandler : IRequestHandler<UpdateProjektCommand>
{
  private readonly IProjektRepository _projektRepository;

  public UpdateProjektCommandHandler(IProjektRepository projektRepository)
  {
    _projektRepository = projektRepository;
  }

  public async Task<Unit> Handle(UpdateProjektCommand request, CancellationToken cancellationToken)
  {
    var create = new CreateProjekt(request.Id, request.Name);
    var projekt = Domain.Projekt.Create(create);
    await _projektRepository.UpdateProjektAsync(projekt, cancellationToken);
    return Unit.Value;
  }
}
