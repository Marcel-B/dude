using com.b_velop.DevIt.Domain;
using DevIt.Projekt.Adapter.Commands;
using DevIt.Repository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevIt.Projekt.Adapter.Handler;

public class CreateProjektCommandHandler : IRequestHandler<CreateProjektCommand, com.b_velop.DevIt.Domain.Projekt>
{
  private readonly IServiceProvider _serviceProvider;

  public CreateProjektCommandHandler(
    IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public async Task<com.b_velop.DevIt.Domain.Projekt> Handle(
    CreateProjektCommand request,
    CancellationToken cancellationToken)
  {
    var create = new CreateProjekt(request.Name, request.ExterneId);
    var projekt = com.b_velop.DevIt.Domain.Projekt.Create(create);
    var uow = _serviceProvider.GetService<IUnitOfWork>();
    var result = await uow.Projekte.CreateProjektAsync(projekt, cancellationToken);
    await uow.CompleteAsync(cancellationToken);
    return result;
  }
}
