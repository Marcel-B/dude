using MediatR;

namespace DevIt.Projekt.Adapter.Commands;

public class DeleteProjektCommand : IRequest
{
  public DeleteProjektCommand(int id)
  {
    Id = id;
  }

  public int Id { get; }
}
