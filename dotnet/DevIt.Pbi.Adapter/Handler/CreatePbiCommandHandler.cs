using DevIt.Pbi.Adapter.Commands;
using DevIt.Persistence;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevIt.Pbi.Adapter.Handler;

public class CreatePbiCommandHandler : IRequestHandler<CreatePbiCommand, Domain.Pbi>
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IServiceProvider _serviceProvider;

  public CreatePbiCommandHandler(
    IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public async Task<Domain.Pbi> Handle(
    CreatePbiCommand request,
    CancellationToken cancellationToken)
  {
    var createCommand = new Domain.Pbi.CreatePbi(request.Name, request.ProjektId);
    var pbi = Domain.Pbi.Create(createCommand);
    using var scope = _serviceProvider.CreateScope();
    var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
    var result = await uow.Pbis.CreatePbiAsync(pbi, cancellationToken);
    await uow.CompleteAsync(cancellationToken);
    return result;
  }
}
