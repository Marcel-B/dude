using DevIt.Abrechnung.Adapter.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DevIt.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AbrechnungController : ControllerBase
{
  private readonly IMediator _mediator;

  public AbrechnungController(IMediator mediator)
  {
    _mediator = mediator;
  }

  [HttpGet("by-monat")]
  public async Task<IActionResult> GetByMonatAsync(
    [FromQuery] int monat,
    [FromQuery] int jahr,
    [FromQuery] string text,
    CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(new GetAbrechnungByMonatQuery(monat, jahr, text), cancellationToken);
    return Ok(new {Stunden = result});
  }

  [HttpGet("by-jahr")]
  public async Task<IActionResult> GetByJahrAsync(
    [FromQuery] int jahr,
    [FromQuery] string text,
    CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(new GetAbrechnungByJahrQuery(jahr, text), cancellationToken);
    return Ok(new {Stunden = result});
  }

  [HttpGet("by-kalenderwoche")]
  public async Task<IActionResult> GetByKalenderwocheAsync(
    [FromQuery] int kalenderwoche,
    [FromQuery] int jahr,
    [FromQuery] string text,
    CancellationToken cancellationToken)
  {
    var result = await _mediator.Send(new GetAbrechnungByKalenderwocheQuery(kalenderwoche, jahr, text),
      cancellationToken);
    return Ok(new {Stunden = result});
  }
}
