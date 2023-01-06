using DevIt.Persistence;
using DevIt.Projekt.Adapter.Query;
using DevIt.Repository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevIt.Projekt.Adapter.Handler;

public class GetProjekteQueryHandler : IRequestHandler<GetProjekteQuery, IEnumerable<Domain.Projekt>>
{
  private readonly IServiceProvider _serviceProvider;

  public GetProjekteQueryHandler(
    IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public async Task<IEnumerable<Domain.Projekt>> Handle(
    GetProjekteQuery request,
    CancellationToken cancellationToken)
  {
    var uow = _serviceProvider.GetRequiredService<IUnitOfWork>();
    var result = await uow.Projekte.GetProjekteAsync(cancellationToken);
    await uow.CompleteAsync(cancellationToken);
    return result;
  }
}
