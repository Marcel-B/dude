using MediatR;

namespace DevIt.Abrechnung.Adapter.Queries;

public class GetAbrechnungByKalenderwocheQuery : IRequest<double>
{
  public int Kalenderwoche { get; }
  public int Jahr { get; }
  public string Text { get; }

  public GetAbrechnungByKalenderwocheQuery(int kalenderwoche, int jahr, string text)
  {
    Kalenderwoche = kalenderwoche;
    Text = text;
    Jahr = jahr;
  }
}
