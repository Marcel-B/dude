using DevIt.Domain;
using DevIt.Persistence;
using DevIt.Projekt.Adapter.Command;
using DevIt.Repository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevIt.Projekt.Adapter.Handler;

public class UpdateProjektCommandHandler : IRequestHandler<UpdateProjektCommand>
{
  private readonly IServiceProvider _serviceProvider;

  public UpdateProjektCommandHandler(
    IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public async Task<Unit> Handle(
    UpdateProjektCommand request,
    CancellationToken cancellationToken)
  {
    var create = new CreateProjekt(request.Id, request.Name);
    var projekt = Domain.Projekt.Create(create);
    var uow = _serviceProvider.GetRequiredService<IUnitOfWork>();
    await uow.Projekte.UpdateProjektAsync(projekt, cancellationToken);
    await uow.CompleteAsync(cancellationToken);
    return Unit.Value;
  }
}
