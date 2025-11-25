using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WasteReporting.API.DTOs;
using WasteReporting.API.Services;

namespace WasteReporting.API.Controllers;

[Route("api/collection-points")]
[ApiController]
[Authorize]
public class CollectionPointsController : ControllerBase
{
    private readonly IManagementService _service;

    public CollectionPointsController(IManagementService service)
    {
        _service = service;
    }

    /// <summary>
    /// Creates a new Collection Point.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<CollectionPointDto>> Create(CreateCollectionPointDto dto)
    {
        var result = await _service.CreateCollectionPointAsync(dto);
        return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
    }
}
