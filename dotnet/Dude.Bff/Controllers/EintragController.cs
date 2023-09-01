using com.b_velop.Dude.Bff.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace com.b_velop.Dude.Bff.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
public class EintragController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetByAsync(
        [FromServices] IEintragService eintragService,
        CancellationToken cancellationToken)
    {
        var result = await eintragService.GetEintraege(cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetByIdAsync(
        [FromRoute] int id,
        CancellationToken cancellationToken)
    {
        return Ok();
    }

    // [HttpPost]
    // public async Task<IActionResult> CreateAsync(
    //     [FromBody] CreateEintragCommand command,
    //     CancellationToken cancellationToken)
    // {
    //     var result = await _mediator.Send(command, cancellationToken);
    //     return Ok(result);
    // }
    //
    // [HttpPut("{id:int}")]
    // public async Task<IActionResult> UpdateAsync(
    //     [FromRoute] int id,
    //     [FromBody] UpdateEintragCommand command,
    //     CancellationToken cancellationToken)
    // {
    //     if (id != command.Id)
    //         throw new BadHttpRequestException("Id in route and body are not equal");
    //     var result = await _mediator.Send(command, cancellationToken);
    //     return Ok(result);
    // }
    //
    // [HttpDelete("{id:int}")]
    // public async Task<IActionResult> DeleteAsync(
    //     [FromRoute] int id,
    //     CancellationToken cancellationToken)
    // {
    //     await _mediator.Send(new DeleteEintragCommand(id), cancellationToken);
    //     return NoContent();
    // }
}