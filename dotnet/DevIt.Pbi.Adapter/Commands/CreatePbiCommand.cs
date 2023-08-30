using MediatR;

namespace DevIt.Pbi.Adapter.Commands;

public class CreatePbiCommand : IRequest<com.b_velop.DevIt.Domain.Pbi>
{
  public string Name { get; }
  public string ProjektId { get; }

  public CreatePbiCommand(string name, string projektId)
  {
    Name = name;
    ProjektId = projektId;
  }
}
