using DevIt.Eintrag.Adapter.Queries;
using DevIt.Persistence;
using MediatR;

namespace DevIt.Eintrag.Adapter.Handler;

public class GetEintraegeQueryHandler : IRequestHandler<GetEintraegeQuery, IList<Domain.Eintrag>>
{
  private readonly IUnitOfWork _uow;

  public GetEintraegeQueryHandler(IUnitOfWork uow)
  {
    _uow = uow;
  }

  public async Task<IList<Domain.Eintrag>> Handle(GetEintraegeQuery request, CancellationToken cancellationToken)
  {
    return await _uow.Eintraege.GetEintraegeAsync(cancellationToken);
  }
}
