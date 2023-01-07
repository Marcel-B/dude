using DevIt.Eintrag.Adapter.Queries;
using DevIt.Persistence;
using MediatR;

namespace DevIt.Eintrag.Adapter.Handler;

public class GetEintragByIdQueryHandler : IRequestHandler<GetEintragByIdQuery, Domain.Eintrag>
{
  private readonly IUnitOfWork _uow;

  public GetEintragByIdQueryHandler(IUnitOfWork uow)
  {
    _uow = uow;
  }

  public async Task<Domain.Eintrag> Handle(GetEintragByIdQuery request, CancellationToken cancellationToken)
  {
    return await _uow.Eintraege.GetEintragByIdAsync(request.Id, cancellationToken);
  }
}
