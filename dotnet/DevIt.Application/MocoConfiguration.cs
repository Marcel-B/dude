namespace DevIt.Application;

public record MocoConfiguration
{
  public string? ApiKey { get; init; }
  public string? Url { get; init; }
  public int From  { get; init; } = DateTime.Now.Month;
  public int To  { get; init; } = DateTime.Now.Month;
  public IList<string> Projekte { get; init; } = new List<string>();
}
