using MediatR;

namespace DevIt.Projekt.Adapter.Query;

public class GetProjektByIdQuery : IRequest<Domain.Projekt>
{
  public GetProjektByIdQuery(string id)
  {
    Id = id;
  }

  public string Id { get; }
}
