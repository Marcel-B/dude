using com.b_velop.Dude.Shared;

namespace com.b_velop.Dude.Bff.Services;

public static class EintragMapperExtensions
{
    public static Eintrag ToSystem(
        this EintragDto eintragDto)
    {
        return new Eintrag(
            eintragDto.Id,
            eintragDto.Text,
            eintragDto.Datum.ToDateTimeOffset()!
                .Value,
            eintragDto.Stunden,
            eintragDto.Abrechenbar,
            eintragDto.ExterneId);
    }
}