using com.b_velop.Dude.Bff.Services;
using com.b_velop.Dude.Bff.UiModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace com.b_velop.Dude.Bff.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
public class GpsController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CoordinateDto>>> GetAsync(
        [FromServices] IGpsService gpsService,
        CancellationToken cancellationToken)
    {
        var result =
            await gpsService.GetCoordinates(
                cancellationToken);
        return Ok(result);
    }
}