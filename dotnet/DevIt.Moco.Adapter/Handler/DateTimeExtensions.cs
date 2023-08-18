using DevIt.Moco.Adapter.Commands;

namespace DevIt.Moco.Adapter.Handler;

public static class DateTimeExtensions
{
  public static DateTimeOffset ToFirstDayOfMonth(this Monat monat)
  {
    var now = DateTimeOffset.Now;
    return new DateTimeOffset(now.Year, (int) monat, 1, 0, 0, 0, now.Offset);
  }

  public static DateTimeOffset ToLastDayOfMonth(this Monat monat)
  {
    var now = DateTimeOffset.Now;
    return new DateTimeOffset(now.Year, (int) monat, DateTime.DaysInMonth(now.Year, (int) monat), 0, 0, 0, now.Offset);
  }
}
