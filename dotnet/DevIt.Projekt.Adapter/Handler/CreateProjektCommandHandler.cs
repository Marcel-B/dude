using DevIt.Domain;
using DevIt.Persistence;
using DevIt.Projekt.Adapter.Command;
using DevIt.Repository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevIt.Projekt.Adapter.Handler;

public class CreateProjektCommandHandler : IRequestHandler<CreateProjektCommand, Domain.Projekt>
{
  private readonly IServiceProvider _serviceProvider;

  public CreateProjektCommandHandler(
    IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public async Task<Domain.Projekt> Handle(
    CreateProjektCommand request,
    CancellationToken cancellationToken)
  {
    var create = new CreateProjekt(request.Id, request.Name);
    var projekt = Domain.Projekt.Create(create);
    var uow = _serviceProvider.GetService<IUnitOfWork>();
    var result = await uow.Projekte.CreateProjektAsync(projekt, cancellationToken);
    await uow.CompleteAsync(cancellationToken);
    return result;
  }
}
