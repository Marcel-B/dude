using DevIt.Pbi.Adapter.Queries;
using DevIt.Persistence;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevIt.Pbi.Adapter.Handler;

public class GetPbiByIdQueryHandler : IRequestHandler<GetPbiByIdQuery, Domain.Pbi>
{
  private readonly IServiceProvider _serviceProvider;

  public GetPbiByIdQueryHandler(
    IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public async Task<Domain.Pbi> Handle(
    GetPbiByIdQuery request,
    CancellationToken cancellationToken)
  {
    using var scope = _serviceProvider.CreateScope();
    var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
    var pbi = await uow.Pbis.GetPbiByIdAsync(request.Id, cancellationToken);
    await uow.CompleteAsync(cancellationToken);
    return pbi;
  }
}
