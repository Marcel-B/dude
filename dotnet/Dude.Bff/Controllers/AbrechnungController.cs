using com.b_velop.Dude.Bff.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace com.b_velop.Dude.Bff.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
public class AbrechnungController : ControllerBase
{
    [HttpGet("by-monat")]
    public async Task<ActionResult<double?>> GetByMonatAsync(
        [FromQuery] int monat,
        [FromQuery] int jahr,
        [FromQuery] string text,
        [FromServices] IAbrechnungService abrechnungService,
        CancellationToken cancellationToken)
    {
        var result = await abrechnungService.GetAbrechnungByMonat(monat, jahr, text, cancellationToken);
        return Ok(new {Stunden = result});
    }

    [HttpGet("by-jahr")]
    public async Task<ActionResult<double?>> GetByJahrAsync(
        [FromQuery] int jahr,
        [FromQuery] string text,
        [FromServices] IAbrechnungService abrechnungService,
        CancellationToken cancellationToken)
    {
        var result = await abrechnungService.GetAbrechnungByJahr(jahr, text, cancellationToken);
        return Ok(new {Stunden = result});
    }

    [HttpGet("by-kalenderwoche")]
    public async Task<IActionResult> GetByKalenderwocheAsync(
        [FromQuery] int kalenderwoche,
        [FromQuery] int jahr,
        [FromQuery] string text,
        [FromServices] IAbrechnungService abrechnungService,
        CancellationToken cancellationToken)
    {
        var result = await abrechnungService.GetAbrechnungByKalenderwoche(kalenderwoche, jahr, text, cancellationToken);
        return Ok(new {Stunden = result});
    }

    [HttpGet("projekte")]
    public async Task<IActionResult> GetProjekteAsync(
        [FromServices] IAbrechnungService abrechnungService,
        CancellationToken cancellationToken)
    {
        var result = await abrechnungService.GetProjekte(cancellationToken);
        return Ok(result);
    }
}