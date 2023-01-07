using MediatR;

namespace DevIt.Eintrag.Adapter.Commands;

public class UpdateEintragCommand : IRequest<Domain.Eintrag>
{
  public int Id { get; }
  public string Text { get; }
  public DateTimeOffset Datum { get; }
  public double Stunden { get; }
  public bool Abrechenbar { get; }

  public UpdateEintragCommand(int id, string text, DateTimeOffset datum, double stunden, bool abrechenbar)
  {
    Id = id;
    Text = text;
    Datum = datum;
    Stunden = stunden;
    Abrechenbar = abrechenbar;
  }
}
