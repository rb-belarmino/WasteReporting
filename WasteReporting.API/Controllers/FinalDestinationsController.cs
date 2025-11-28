using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WasteReporting.API.ViewModels;
using WasteReporting.API.Services;

namespace WasteReporting.API.Controllers;

[Route("api/final-destinations")]
[ApiController]
[Authorize]
public class FinalDestinationsController : ControllerBase
{
    private readonly IManagementService _service;

    public FinalDestinationsController(IManagementService service)
    {
        _service = service;
    }

    /// <summary>
    /// Creates a new Final Destination.
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<FinalDestinationViewModel>> Create(CreateFinalDestinationViewModel dto)
    {
        var result = await _service.CreateFinalDestinationAsync(dto);
        return CreatedAtAction(nameof(Create), new { id = result.Id }, result);
    }
}
