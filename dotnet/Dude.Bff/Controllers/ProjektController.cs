using com.b_velop.Dude.Bff.Services;
using com.b_velop.Dude.Bff.UiModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace com.b_velop.Dude.Bff.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
public class ProjektController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Projekt>>> GetAsync(
        [FromServices] IProjektService projektService,
        CancellationToken cancellationToken)
    {
        var result = await projektService.GetProjekte(cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Projekt>> GetByIdAsync(
        [FromRoute] int id,
        [FromServices] IProjektService projektService,
        CancellationToken cancellationToken)
    {
        var result = await projektService.GetProjektById(id, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Projekt>> CreateAsync(
        [FromBody] Projekt projekt,
        [FromServices] IProjektService projektService,
        CancellationToken cancellationToken)
    {
        var result = await projektService.CreateProjekt(projekt, cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(
        [FromRoute] int id,
        [FromBody] Projekt projekt,
        [FromServices] IProjektService projektService,
        CancellationToken cancellationToken)
    {
        if (id != projekt.Id)
            throw new BadHttpRequestException("Id in route and body are not equal");
        var result = await projektService.UpdateProjekt(projekt, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] int id,
        [FromServices] IProjektService projektService,
        CancellationToken cancellationToken)
    {
        await projektService.DeleteProjekt(id, cancellationToken);
        return NoContent();
    }
}