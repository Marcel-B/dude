using MediatR;

namespace DevIt.Projekt.Adapter.Command;

public class CreateProjektCommand : IRequest
{
  public CreateProjektCommand(string id, string name)
  {
    Id = id;
    Name = name;
  }

  public string Id { get; }
  public string Name { get; }
}
