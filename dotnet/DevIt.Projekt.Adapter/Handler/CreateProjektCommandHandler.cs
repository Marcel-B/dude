using DevIt.Projekt.Adapter.Command;
using DevIt.Repository;
using MediatR;

namespace DevIt.Projekt.Adapter.Handler;

public class CreateProjektCommandHandler : IRequestHandler<CreateProjektCommand>
{
  private readonly IProjektRepository _projektRepository;

  public CreateProjektCommandHandler(IProjektRepository projektRepository)
  {
    _projektRepository = projektRepository;
  }

  public async Task<Unit> Handle(CreateProjektCommand request, CancellationToken cancellationToken)
  {
    var projekt = new Domain.Projekt
    {
      Id = request.Id,
      Name = request.Name
    };

    await _projektRepository.CreateProjektAsync(projekt, cancellationToken);
    return Unit.Value;
  }
}
