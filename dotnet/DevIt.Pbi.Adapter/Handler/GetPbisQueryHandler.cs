using DevIt.Pbi.Adapter.Queries;
using DevIt.Persistence;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevIt.Pbi.Adapter.Handler;

public class GetPbisQueryHandler : IRequestHandler<GetPbisQuery, IList<Domain.Pbi>>
{
  private readonly IServiceProvider _serviceProvider;

  public GetPbisQueryHandler(
    IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public async Task<IList<Domain.Pbi>> Handle(
    GetPbisQuery request,
    CancellationToken cancellationToken)
  {
    using var scope = _serviceProvider.CreateScope();
    var uow = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
    var pbis = await uow.Pbis.GetPbisAsync(cancellationToken);
    await uow.CompleteAsync(cancellationToken);
    return pbis;
  }
}
