using MediatR;

namespace DevIt.Abrechnung.Adapter.Queries;

public class GetAbrechnungByMonatQuery : IRequest<double>
{
  public int Monat { get; }
  public int Jahr { get; }
  public string Text { get; }

  public GetAbrechnungByMonatQuery(int monat, int jahr, string text)
  {
    Monat = monat;
    Jahr = jahr;
    Text = text;
  }
}
