using DevIt.Projekt.Adapter.Query;
using DevIt.Repository;
using MediatR;

namespace DevIt.Projekt.Adapter.Handler;

public class GetProjekteQueryHandler : IRequestHandler<GetProjekteQuery, IEnumerable<Domain.Projekt>>
{
  private readonly IProjektRepository _projektRepository;

  public GetProjekteQueryHandler(
    IProjektRepository projektRepository)
  {
    _projektRepository = projektRepository;
  }

  public async Task<IEnumerable<Domain.Projekt>> Handle(
    GetProjekteQuery request,
    CancellationToken cancellationToken)
  {
    return await _projektRepository.GetProjekteAsync(cancellationToken);
  }
}
public class GetProjektByIdQueryHandler : IRequestHandler<GetProjektByIdQuery, Domain.Projekt>
{
  private readonly IProjektRepository _projektRepository;

  public GetProjektByIdQueryHandler(
    IProjektRepository projektRepository)
  {
    _projektRepository = projektRepository;
  }


  public async Task<Domain.Projekt> Handle(GetProjektByIdQuery request, CancellationToken cancellationToken)
  {
    var result = await _projektRepository.GetProjektByIdAsync(request.Id, cancellationToken);
    return result;
  }
}
