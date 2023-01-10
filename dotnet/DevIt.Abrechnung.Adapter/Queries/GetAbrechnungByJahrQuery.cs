using MediatR;

namespace DevIt.Abrechnung.Adapter.Queries;

public class GetAbrechnungByJahrQuery : IRequest<double>
{
  public int Jahr { get; }
  public string Text { get; }

  public GetAbrechnungByJahrQuery(int jahr, string text)
  {
    Jahr = jahr;
    Text = text;
  }
}
