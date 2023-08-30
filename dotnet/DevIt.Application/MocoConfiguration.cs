namespace DevIt.Application;

public record MocoConfiguration
{
  public string? ApiKey { get; init; }
  public string? Url { get; init; }
  public IList<string> Projekte { get; init; } = new List<string>();
}
