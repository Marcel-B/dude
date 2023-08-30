using MediatR;

namespace DevIt.Projekt.Adapter.Queries;

public class GetProjektByIdQuery : IRequest<com.b_velop.DevIt.Domain.Projekt>
{
  public GetProjektByIdQuery(string id)
  {
    Id = id;
  }

  public string Id { get; }
}
