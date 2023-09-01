using com.b_velop.Dude.Bff.Services;
using com.b_velop.Dude.Bff.UiModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace com.b_velop.Dude.Bff.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
public class DeviceController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<SelectItem>>> GetDevices(
        [FromServices] MeasurementService measurementService,
        CancellationToken cancellationToken)
    {
        var result =
            await measurementService.GetDevices(
                cancellationToken);
        return Ok(result);
    }

    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult<Device>> GetDeviceById(
        [FromRoute] Guid id,
        [FromServices] MeasurementService measurementService,
        CancellationToken cancellationToken)
    {
        var result =
            await measurementService.GetDeviceById(id,
                cancellationToken);
        return Ok(result);
    }
}