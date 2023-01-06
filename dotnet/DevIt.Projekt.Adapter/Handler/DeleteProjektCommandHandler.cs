using DevIt.Persistence;
using DevIt.Projekt.Adapter.Command;
using DevIt.Repository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevIt.Projekt.Adapter.Handler;

public class DeleteProjektCommandHandler : IRequestHandler<DeleteProjektCommand>
{
  private readonly IServiceProvider _serviceProvider;

  public DeleteProjektCommandHandler(
    IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public async Task<Unit> Handle(DeleteProjektCommand request, CancellationToken cancellationToken)
  {
    var uow = _serviceProvider.GetRequiredService<IUnitOfWork>();
    await uow.Projekte.DeleteProjektAsync(request.Id, cancellationToken);
    await uow.CompleteAsync(cancellationToken);
    return Unit.Value;
  }
}
