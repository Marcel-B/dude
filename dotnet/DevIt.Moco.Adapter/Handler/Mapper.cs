using b_velop;
using com.b_velop.DevIt.Domain;

namespace DevIt.Moco.Adapter.Handler;

public static class Mapper
{
  public static Eintrag ToEintrag(this Activity activity)
  {

    var cmd = new Eintrag.CreateEintrag(
      "Solverest",
      activity.Hours,
      activity.Date,
      true,
      activity.Id.ToString());

    return Eintrag.Create(cmd);
  }
}
