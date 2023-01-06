using DevIt.Pbi.Adapter.Commands;
using DevIt.Pbi.Adapter.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevIt.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PbiController : ControllerBase
{
  private readonly IMediator _mediator;

  public PbiController(IMediator mediator)
  {
    _mediator = mediator;
  }

  [HttpGet]
  public async Task<IActionResult> GetByAsync(CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(new GetPbisQuery(), cancellationToken);
    return Ok(result);
  }

  [HttpGet("{id:int}")]
  public async Task<IActionResult> GetByIdAsync(
    [FromRoute] int id,
    CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(new GetPbiByIdQuery(id), cancellationToken);
    return Ok(result);
  }

  [HttpPost]
  public async Task<IActionResult> CreateAsync(
    [FromBody] CreatePbiCommand command,
    CancellationToken cancellationToken)
  {
    await _mediator.Send(command, cancellationToken);
    return Ok();
  }

  [HttpPut("{id:int}")]
  public async Task<IActionResult> UpdateAsync(
    [FromRoute] int id,
    [FromBody] UpdatePbiCommand command,
    CancellationToken cancellationToken)
  {
    if(id != command.Id)
      throw new BadHttpRequestException("Id in route and body are not equal");
    await _mediator.Send(command, cancellationToken);
    return Ok();
  }

  [HttpDelete("{id:int}")]
  public async Task<IActionResult> DeleteAsync(
    [FromRoute] int id,
    CancellationToken cancellationToken)
  {
    await _mediator.Send(new DeletePbiCommand(id), cancellationToken);
    return NoContent();
  }
}
