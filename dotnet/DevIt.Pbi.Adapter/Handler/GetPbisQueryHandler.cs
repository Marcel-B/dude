using DevIt.Pbi.Adapter.Queries;
using DevIt.Repository;
using MediatR;

namespace DevIt.Pbi.Adapter.Handler;

public class GetPbisQueryHandler : IRequestHandler<GetPbisQuery, IList<Domain.Pbi>>
{
  private readonly IPbiRepository _pbiRepository;

  public GetPbisQueryHandler(IPbiRepository pbiRepository)
  {
    _pbiRepository = pbiRepository;
  }

  public async Task<IList<Domain.Pbi>> Handle(
    GetPbisQuery request,
    CancellationToken cancellationToken)
  {
    var pbis = await _pbiRepository.GetPbisAsync(cancellationToken);
    return pbis;
  }
}
