using MediatR;

namespace DevIt.Projekt.Adapter.Queries;

public class GetProjektByIdQuery : IRequest<com.b_velop.DevIt.Domain.Projekt>
{
  public GetProjektByIdQuery(int id)
  {
    Id = id;
  }

  public int Id { get; }
}
