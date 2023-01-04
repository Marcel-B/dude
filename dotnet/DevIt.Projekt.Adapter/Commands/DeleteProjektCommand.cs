using MediatR;

namespace DevIt.Projekt.Adapter.Command;

public class DeleteProjektCommand : IRequest
{
  public DeleteProjektCommand(string id)
  {
    Id = id;
  }

  public string Id { get; }
}
