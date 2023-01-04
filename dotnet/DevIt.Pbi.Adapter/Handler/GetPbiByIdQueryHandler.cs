using DevIt.Pbi.Adapter.Queries;
using DevIt.Repository;
using MediatR;

namespace DevIt.Pbi.Adapter.Handler;

public class GetPbiByIdQueryHandler : IRequestHandler<GetPbiByIdQuery, Domain.Pbi>
{
  private readonly IPbiRepository _pbiRepository;

  public GetPbiByIdQueryHandler(IPbiRepository pbiRepository)
  {
    _pbiRepository = pbiRepository;
  }

  public async Task<Domain.Pbi> Handle(
    GetPbiByIdQuery request,
    CancellationToken cancellationToken)
  {
    var pbi = await _pbiRepository.GetPbiByIdAsync(request.Id, cancellationToken);
    return pbi;
  }
}
