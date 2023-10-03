using DevIt.Projekt.Adapter.Queries;
using DevIt.Repository;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DevIt.Projekt.Adapter.Handler;

public class GetProjekteQueryHandler : IRequestHandler<GetProjekteQuery, IEnumerable<com.b_velop.DevIt.Domain.Projekt>>
{
  private readonly IServiceProvider _serviceProvider;

  public GetProjekteQueryHandler(
    IServiceProvider serviceProvider)
  {
    _serviceProvider = serviceProvider;
  }

  public async Task<IEnumerable<com.b_velop.DevIt.Domain.Projekt>> Handle(
    GetProjekteQuery request,
    CancellationToken cancellationToken)
  {
    var uow = _serviceProvider.GetRequiredService<IUnitOfWork>();
    var result = await uow.Projekte.GetProjekteAsync(cancellationToken);
    await uow.CompleteAsync(cancellationToken);
    return result;
  }
}
