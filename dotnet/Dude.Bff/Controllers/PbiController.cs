using com.b_velop.Dude.Bff.Services;
using com.b_velop.Dude.Bff.UiModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace com.b_velop.Dude.Bff.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
public class PbiController : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pbi>>> GetAsync(
        [FromServices] IPbiService pbiService,
        CancellationToken cancellationToken)
    {
        var result = await pbiService.GetPbis(cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<Pbi>> GetByIdAsync(
        [FromRoute] int id,
        [FromServices] IPbiService pbiService,
        CancellationToken cancellationToken)
    {
        var result = await pbiService.GetPbiById(id, cancellationToken);
        return Ok(result);
    }

    [HttpPost]
    public async Task<ActionResult<Pbi>> CreateAsync(
        [FromBody] Pbi pbi,
        [FromServices] IPbiService pbiService,
        CancellationToken cancellationToken)
    {
        var result = await pbiService.CreatePbi(pbi, cancellationToken);
        return Ok(result);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateAsync(
        [FromRoute] int id,
        [FromBody] Pbi pbi,
        [FromServices] IPbiService pbiService,
        CancellationToken cancellationToken)
    {
        if (id != pbi.Id)
            throw new BadHttpRequestException("Id in route and body are not equal");
        var result = await pbiService.UpdatePbi(pbi, cancellationToken);
        return Ok(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(
        [FromRoute] int id,
        [FromServices] IPbiService pbiService,
        CancellationToken cancellationToken)
    {
        await pbiService.DeletePbi(id, cancellationToken);
        return NoContent();
    }
}