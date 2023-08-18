using MediatR;

namespace DevIt.Moco.Adapter.Commands;

public class CreateEintraegeByMocoCommand : IRequest
{
  public IList<string> ProjektIds { get; set; }
  public Monat? Monat { get; set; }
}
