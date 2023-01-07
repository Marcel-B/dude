using MediatR;

namespace DevIt.Eintrag.Adapter.Queries;

public class GetEintragByIdQuery : IRequest<Domain.Eintrag>
{
  public int Id { get; }

  public GetEintragByIdQuery(int id)
  {
    Id = id;
  }
}
