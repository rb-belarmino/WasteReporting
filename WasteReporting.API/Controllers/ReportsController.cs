using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WasteReporting.API.DTOs;
using WasteReporting.API.Services;

namespace WasteReporting.API.Controllers;

[ApiController]
[Route("api/reports")]
[Authorize]
public class ReportsController : ControllerBase
{
    private readonly IReportService _service;

    public ReportsController(IReportService service)
    {
        _service = service;
    }

    /// <summary>
    /// Creates a new report.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ReportResponseDto>> CreateReport([FromBody] CreateReportDto dto)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await _service.CreateReportAsync(dto, userId);
        return CreatedAtAction(nameof(ListMyReports), new { id = result.Id }, result);
    }

    /// <summary>
    /// Lists reports created by the logged-in user.
    /// </summary>
    [HttpGet("my-reports")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<ReportResponseDto>>> ListMyReports([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        var result = await _service.ListMyReportsAsync(userId, page, pageSize);
        return Ok(result);
    }

    /// <summary>
    /// Lists all reports (Admin only).
    /// </summary>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<ReportResponseDto>>> ListAllReports([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _service.ListAllReportsAsync(page, pageSize);
        return Ok(result);
    }

    /// <summary>
    /// Updates the status of a report (Admin only).
    /// </summary>
    /// <param name="id">Report ID.</param>
    /// <param name="status">New status (e.g., "RESOLVED").</param>
    /// <returns>The updated report.</returns>
    [HttpPut("{id}/status")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ReportResponseDto>> UpdateStatus(int id, [FromBody] string status)
    {
        var result = await _service.UpdateStatusAsync(id, status);
        return Ok(result);
    }
}
