using MediatR;

namespace DevIt.Projekt.Adapter.Commands;

public class UpdateProjektCommand : IRequest<com.b_velop.DevIt.Domain.Projekt>
{
  public UpdateProjektCommand(string id, string name)
  {
    Id = id;
    Name = name;
  }

  public string Id { get; }
  public string Name { get; }
}
