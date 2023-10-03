using MediatR;

namespace DevIt.Pbi.Adapter.Queries;

public class GetPbiByIdQuery : IRequest<com.b_velop.DevIt.Domain.Pbi>
{
  public int Id { get; }

  public GetPbiByIdQuery(int id)
  {
    Id = id;
  }
}
