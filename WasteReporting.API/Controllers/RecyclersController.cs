using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WasteReporting.API.ViewModels;
using WasteReporting.API.Services;

namespace WasteReporting.API.Controllers;

[Route("api/recyclers")]
[ApiController]
[Authorize]
public class RecyclersController : ControllerBase
{
    private readonly IManagementService _service;

    public RecyclersController(IManagementService service)
    {
        _service = service;
    }

    /// <summary>
    /// Creates a new Recycler.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<RecyclerViewModel>> Create(CreateRecyclerViewModel dto)
    {
        var result = await _service.CreateRecyclerAsync(dto);
        return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
    }
}
