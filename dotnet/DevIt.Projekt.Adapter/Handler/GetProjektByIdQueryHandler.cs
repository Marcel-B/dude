using DevIt.Persistence;
using DevIt.Projekt.Adapter.Query;
using DevIt.Repository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevIt.Projekt.Adapter.Handler;

public class GetProjektByIdQueryHandler : IRequestHandler<GetProjektByIdQuery, Domain.Projekt>
{
  private readonly IServiceProvider _serviceProvider;

  public GetProjektByIdQueryHandler(
    IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public async Task<Domain.Projekt> Handle(
    GetProjektByIdQuery request,
    CancellationToken cancellationToken)
  {
    var uow = _serviceProvider.GetRequiredService<IUnitOfWork>();
    var result = await uow.Projekte.GetProjektByIdAsync(request.Id, cancellationToken);
    await uow.CompleteAsync(cancellationToken);
    return result;
  }
}
