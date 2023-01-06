using DevIt.Pbi.Adapter.Commands;
using DevIt.Persistence;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevIt.Pbi.Adapter.Handler;

public class DeletePbiCommandHandler : IRequestHandler<DeletePbiCommand>
{
  private readonly IServiceProvider _serviceProvider;

  public DeletePbiCommandHandler(IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public async Task<Unit> Handle(DeletePbiCommand request, CancellationToken cancellationToken)
  {
    using var scope = _serviceProvider.CreateScope();
    var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
    await unitOfWork.Pbis.DeletePbiAsync(request.Id, cancellationToken);
    await unitOfWork.CompleteAsync(cancellationToken);
    return Unit.Value;
  }
}
