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
    [Route("api/sensors")]
    public async Task<IActionResult> GetSensors(
        CancellationToken cancellationToken)
    {
        var result =
            await _measurementService.GetSensors(
                cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    [Route("api/devices")]
    public async Task<IActionResult> GetDevices(
        CancellationToken cancellationToken)
    {
        var result =
            await _measurementService.GetDevices(
                cancellationToken);
        return Ok(result);
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

    [HttpGet]
    [Route("api/device/{id:guid}")]
    public async Task<IActionResult> GetDeviceById(
        [FromRoute] Guid id,
        CancellationToken cancellationToken)
    {
        var result =
            await _measurementService.GetDeviceById(id,
                cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    [Route("api/measurements/{sensorId:guid}")]
    public async Task<IActionResult> GetMeasurementBySensorId(
        [FromRoute] Guid sensorId,
        [FromQuery] DateTimeOffset? from,
        [FromQuery] DateTimeOffset? to,
        CancellationToken cancellationToken)
    {
        var result =
            await _measurementService.GetMeasurementBySensorIdAsync(sensorId,
                from, to,
                cancellationToken);
        return Ok(result);
    }
}