using DevIt.Moco.Adapter.Commands;

namespace DevIt.Moco.Adapter.Handler;

public static class DateTimeExtensions
{
  public static DateTimeOffset ToFirstDayOfMonth(this Monat monat, int? year = null)
  {
    var now = DateTimeOffset.Now;
    return new DateTimeOffset(year ?? now.Year, (int) monat, 1, 0, 0, 0, now.Offset);
  }

  public static DateTimeOffset ToLastDayOfMonth(this Monat monat, int? year = null)
  {
    var now = DateTimeOffset.Now;
    return new DateTimeOffset(year ?? now.Year, (int) monat, DateTime.DaysInMonth(now.Year, (int) monat), 0, 0, 0, now.Offset);
  }
}
