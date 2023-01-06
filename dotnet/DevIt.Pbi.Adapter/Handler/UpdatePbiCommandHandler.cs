using DevIt.Pbi.Adapter.Commands;
using DevIt.Persistence;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevIt.Pbi.Adapter.Handler;

public class UpdatePbiCommandHandler : IRequestHandler<UpdatePbiCommand, Domain.Pbi>
{
  private readonly IServiceProvider _serviceProvider;

  public UpdatePbiCommandHandler(
    IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public async Task<Domain.Pbi> Handle(
    UpdatePbiCommand request,
    CancellationToken cancellationToken)
  {
    using var scope = _serviceProvider.CreateScope();
    var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
    var createCommand = new Domain.Pbi.CreatePbi(request.Name, request.ProjektId, request.Id);
    var pbi = Domain.Pbi.Create(createCommand);
    var result = await uow.Pbis.UpdatePbiAsync(pbi, cancellationToken);
    await uow.CompleteAsync(cancellationToken);
    return result;
  }
}
