using com.b_velop.Dude.Bff.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace com.b_velop.Dude.Bff.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
public class SensorController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetSensors(
        [FromServices] MeasurementService measurementService,
        CancellationToken cancellationToken)
    {
        var result =
            await measurementService.GetSensors(
                cancellationToken);
        return Ok(result);
    }
}