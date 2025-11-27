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
        var result = await _service.GetCollectionByIdAsync(id);
        return Ok(result);
    }

    /// <summary>
    /// Lists all collections.
    /// </summary>
    /// <summary>
    /// Lists all collections.
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CollectionResponseDto>>> ListAll([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _service.ListCollectionsAsync(page, pageSize);
        return Ok(result);
    }

    /// <summary>
    /// Updates a collection.
    /// </summary>
    [HttpPut("{id}")]
    [HttpPut("{id}")]
    public async Task<ActionResult<CollectionResponseDto>> Update(int id, UpdateCollectionDto dto)
    {
        var result = await _service.UpdateCollectionAsync(id, dto);
        return Ok(result);
    }

    /// <summary>
    /// Deletes a collection.
    /// </summary>
    [HttpDelete("{id}")]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
    {
        await _service.DeleteCollectionAsync(id);
        return NoContent();
    }
}
