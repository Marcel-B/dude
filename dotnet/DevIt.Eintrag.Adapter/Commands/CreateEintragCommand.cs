using MediatR;

namespace DevIt.Eintrag.Adapter.Commands;

public class CreateEintragCommand : IRequest<com.b_velop.DevIt.Domain.Eintrag>
{
  public string Text { get; }
  public DateTimeOffset Datum { get; }
  public double Stunden { get; }
  public bool Abrechenbar { get; }

  public CreateEintragCommand(string text, DateTimeOffset datum, double stunden, bool abrechenbar)
  {
    Text = text;
    Datum = datum;
    Stunden = stunden;
    Abrechenbar = abrechenbar;
  }
}
