using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WasteReporting.API.DTOs;
using WasteReporting.API.Services;

namespace WasteReporting.API.Controllers;

[Route("api/collections")]
[ApiController]
[Authorize]
public class CollectionsController : ControllerBase
{
    private readonly ICollectionService _service;

    public CollectionsController(ICollectionService service)
    {
        _service = service;
    }

    /// <summary>
    /// Schedules a new collection.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<CollectionResponseDto>> Schedule(CreateCollectionDto dto)
    {
        var result = await _service.ScheduleCollectionAsync(dto);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    /// <summary>
    /// Gets a collection by ID.
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<CollectionResponseDto>> GetById(int id)
    {
        try
        {
            var result = await _service.GetCollectionByIdAsync(id);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Lists all collections.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CollectionResponseDto>>> ListAll()
    {
        var result = await _service.ListCollectionsAsync();
        return Ok(result);
    }

    /// <summary>
    /// Updates a collection.
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<CollectionResponseDto>> Update(int id, UpdateCollectionDto dto)
    {
        try
        {
            var result = await _service.UpdateCollectionAsync(id, dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Deletes a collection.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            await _service.DeleteCollectionAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return NotFound(new { message = ex.Message });
        }
    }
}
