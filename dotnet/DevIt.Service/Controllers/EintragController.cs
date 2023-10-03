using DevIt.Eintrag.Adapter.Commands;
using DevIt.Eintrag.Adapter.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace com.b_velop.DevIt.Service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EintragController : ControllerBase
{
  private readonly IMediator _mediator;

  public EintragController(IMediator mediator)
  {
    _mediator = mediator;
  }

  // [HttpGet]
  // public async Task<IActionResult> GetByAsync(CancellationToken cancellationToken)
  // {
  //   var result = await _mediator.Send(new GetEintraegeQuery(), cancellationToken);
  //   return Ok(result);
  // }

  // [HttpGet("{id:int}")]
  // public async Task<IActionResult> GetByIdAsync(
  //   [FromRoute] int id,
  //   CancellationToken cancellationToken)
  // {
  //   var result = await _mediator.Send(new GetEintragByIdQuery(id), cancellationToken);
  //   return Ok(result);
  // }

  // [HttpPost]
  // public async Task<IActionResult> CreateAsync(
  //   [FromBody] CreateEintragCommand command,
  //   CancellationToken cancellationToken)
  // {
  //   var result = await _mediator.Send(command, cancellationToken);
  //   return Ok(result);
  // }
  //
  // [HttpPut("{id:int}")]
  // public async Task<IActionResult> UpdateAsync(
  //   [FromRoute] int id,
  //   [FromBody] UpdateEintragCommand command,
  //   CancellationToken cancellationToken)
  // {
  //   if (id != command.Id)
  //     throw new BadHttpRequestException("Id in route and body are not equal");
  //   var result = await _mediator.Send(command, cancellationToken);
  //   return Ok(result);
  // }
  //
  // [HttpDelete("{id:int}")]
  // public async Task<IActionResult> DeleteAsync(
  //   [FromRoute] int id,
  //   CancellationToken cancellationToken)
  // {
  //   await _mediator.Send(new DeleteEintragCommand(id), cancellationToken);
  //   return NoContent();
  // }
}
