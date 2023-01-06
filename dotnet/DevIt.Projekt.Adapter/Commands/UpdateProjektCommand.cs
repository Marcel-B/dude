using MediatR;

namespace DevIt.Projekt.Adapter.Command;

public class UpdateProjektCommand : IRequest<Domain.Projekt>
{
  public UpdateProjektCommand(string id, string name)
  {
    Id = id;
    Name = name;
  }

  public string Id { get; }
  public string Name { get; }
}
