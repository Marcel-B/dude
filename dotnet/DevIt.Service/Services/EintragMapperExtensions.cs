using com.b_velop.DevIt.Domain;
using com.b_velop.Dude.Shared;

namespace com.b_velop.DevIt.Service.Services;

public static class EintragMapperExtensions
{
    public static EintragDto ToProto(
        this Eintrag eintrag)
    {
        return new EintragDto
        {
            Id = eintrag.Id,
            Abrechenbar = eintrag.Abrechenbar,
            Datum = eintrag.Datum.ToProto(),
            ExterneId = eintrag.ExterneId ?? string.Empty,
            Stunden = eintrag.Stunden,
            Text = eintrag.Text
        };
    }
}