using DevIt.Projekt.Adapter.Command;
using DevIt.Projekt.Adapter.Query;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevIt.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjektController : ControllerBase
{
  private readonly IMediator _mediator;

  public ProjektController(IMediator mediator)
  {
    _mediator = mediator;
  }

  [HttpGet]
  public async Task<IActionResult> GetByAsync(CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(new GetProjekteQuery(), cancellationToken);
    return Ok(result);
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> GetByIdAsync(
    [FromRoute] string id,
    CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(new GetProjektByIdQuery(id), cancellationToken);
    return Ok(result);
  }

  [HttpPost]
  public async Task<IActionResult> CreateAsync(
    [FromBody] CreateProjektCommand command,
    CancellationToken cancellationToken)
  {
    await _mediator.Send(command, cancellationToken);
    return Ok();
  }

  [HttpPut("{id}")]
  public async Task<IActionResult> UpdateAsync(
    [FromRoute] string id,
    [FromBody] UpdateProjektCommand command,
    CancellationToken cancellationToken)
  {
    await _mediator.Send(command, cancellationToken);
    return Ok();
  }

  [HttpDelete("{id}")]
  public async Task<IActionResult> DeleteAsync(
    [FromRoute] string id,
    CancellationToken cancellationToken)
  {
    await _mediator.Send(new DeleteProjektCommand(id), cancellationToken);
    return NoContent();
  }
}