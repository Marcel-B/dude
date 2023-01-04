using DevIt.Projekt.Adapter.Query;
using DevIt.Repository;
using MediatR;

namespace DevIt.Projekt.Adapter.Handler;

public class GetProjektByIdQueryHandler : IRequestHandler<GetProjektByIdQuery, Domain.Projekt>
{
  private readonly IProjektRepository _projektRepository;

  public GetProjektByIdQueryHandler(IProjektRepository projektRepository)
  {
    _projektRepository = projektRepository;
  }

  public async Task<Domain.Projekt> Handle(GetProjektByIdQuery request, CancellationToken cancellationToken)
  {
    return await _projektRepository.GetProjektByIdAsync(request.Id, cancellationToken);
  }
}
