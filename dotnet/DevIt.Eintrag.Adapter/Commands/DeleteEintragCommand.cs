using MediatR;

namespace DevIt.Eintrag.Adapter.Commands;

public class DeleteEintragCommand : IRequest
{
  public int Id { get; }

  public DeleteEintragCommand(int id)
  {
    Id = id;
  }
}
