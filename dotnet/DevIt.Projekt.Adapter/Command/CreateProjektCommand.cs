using MediatR;

namespace DevIt.Projekt.Adapter.Command;

public class CreateProjektCommand : IRequest
{
  public string Id { get; set; }
  public string Name { get; set; }
}
