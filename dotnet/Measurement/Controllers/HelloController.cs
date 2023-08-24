using com.b_velop.Measurement.Services;
using Microsoft.AspNetCore.Mvc;

namespace com.b_velop.Measurement.Controllers;

[ApiController]
public class HelloController : ControllerBase
{
    private readonly MeasurementService _measurementService;

    public HelloController(
        MeasurementService measurementService)
    {
        _measurementService = measurementService;
    }

    [HttpGet]
    [Route("api/hello/{id}")]
    public async Task<IActionResult> Get(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result =
            await _measurementService.GetMeasurement(id,
                cancellationToken);
        return Ok(result);
    }
}