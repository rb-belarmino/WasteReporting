using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WasteReporting.API.DTOs;
using WasteReporting.API.Services;

namespace WasteReporting.API.Controllers;

[Route("api/collection-wastes")]
[ApiController]
[Authorize]
public class CollectionWastesController : ControllerBase
{
    private readonly ICollectionService _service;

    public CollectionWastesController(ICollectionService service)
    {
        _service = service;
    }

    /// <summary>
    /// Associates a waste type with a collection.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult> Associate(CreateCollectionWasteDto dto)
    {
        await _service.AssociateWasteAsync(dto);
        return Ok(new { message = "Waste associated successfully." });
    }

    /// <summary>
    /// Disassociates a waste type from a collection.
    /// </summary>
    [HttpDelete("{collectionId}/{wasteId}")]
    public async Task<ActionResult> Disassociate(int collectionId, int wasteId)
    {
        try
        {
            await _service.DisassociateWasteAsync(collectionId, wasteId);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Lists all associations between collections and wastes.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CollectionWasteResponseDto>>> ListAll()
    {
        var result = await _service.ListAssociationsAsync();
        return Ok(result);
    }
}
