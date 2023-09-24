using MediatR;

namespace DevIt.Moco.Adapter.Commands;

public class CreateEintraegeByMocoCommand : IRequest
{
  public IList<string> ProjektIds { get; set; }
  public Monat From { get; set; }
  public Monat To { get; set; }
}
