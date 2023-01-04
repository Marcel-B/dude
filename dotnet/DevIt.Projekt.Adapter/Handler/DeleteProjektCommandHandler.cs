using DevIt.Projekt.Adapter.Command;
using DevIt.Repository;
using MediatR;

namespace DevIt.Projekt.Adapter.Handler;

public class DeleteProjektCommandHandler : IRequestHandler<DeleteProjektCommand>
{
  private readonly IProjektRepository _projektRepository;

  public DeleteProjektCommandHandler(IProjektRepository projektRepository)
  {
    _projektRepository = projektRepository;
  }

  public async Task<Unit> Handle(DeleteProjektCommand request, CancellationToken cancellationToken)
  {
    await _projektRepository.DeleteProjektAsync(request.Id, cancellationToken);
    return Unit.Value;
  }
}
