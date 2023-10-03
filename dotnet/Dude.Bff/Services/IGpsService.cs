using com.b_velop.Dude.Bff.UiModel;

namespace com.b_velop.Dude.Bff.Services;

public interface IGpsService
{
    Task<IEnumerable<CoordinateDto>> GetCoordinates(
        CancellationToken cancellationToken = default);
}