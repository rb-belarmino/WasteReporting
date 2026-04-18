using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WasteReporting.API.ViewModels;
using WasteReporting.API.Services;

namespace WasteReporting.API.Controllers;

[Route("api/wastes")]
[ApiController]
[Authorize]
public class WastesController : ControllerBase
{
    private readonly IManagementService _service;

    public WastesController(IManagementService service)
    {
        _service = service;
    }

    /// <summary>
    /// Creates a new Waste type.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<WasteViewModel>> Create(CreateWasteViewModel dto)
    {
        var result = await _service.CreateWasteAsync(dto);
        return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
    }

    /// <summary>
    /// Lists all registered waste types.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<WasteViewModel>>> ListAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _service.ListWastesAsync(page, pageSize);
        return Ok(result);
    }
}
