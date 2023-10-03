using com.b_velop.Dude.Bff.Services;
using com.b_velop.Dude.Bff.UiModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace com.b_velop.Dude.Bff.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
public class MeasurementController : ControllerBase
{
    [HttpGet]
    [Route("sensor/{sensorId:guid}")]
    public async Task<IActionResult> GetMeasurementBySensorId(
        [FromRoute] Guid sensorId,
        [FromQuery] DateTimeOffset? from,
        [FromQuery] DateTimeOffset? to,
        [FromServices] MeasurementService measurementService,
        CancellationToken cancellationToken)
    {
        var result =
            await measurementService.GetMeasurementBySensorIdAsync(sensorId,
                from, to,
                cancellationToken);
        return Ok(result);
    }
}