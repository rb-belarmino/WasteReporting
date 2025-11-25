using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WasteReporting.API.DTOs;
using WasteReporting.API.Services;

namespace WasteReporting.API.Controllers;

[ApiController]
[Route("api/denuncias")]
[Authorize]
public class DenunciasController : ControllerBase
{
    private readonly IDenunciaService _denunciaService;

    public DenunciasController(IDenunciaService denunciaService)
    {
        _denunciaService = denunciaService;
    }

    [HttpPost]
    public async Task<ActionResult<DenunciaResponseDto>> CriarDenuncia(CreateDenunciaDto dto)
    {
        try
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _denunciaService.CriarDenunciaAsync(dto, userId);
            return CreatedAtAction(nameof(ListarMinhasDenuncias), result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("minhas")]
    public async Task<ActionResult<IEnumerable<DenunciaResponseDto>>> ListarMinhasDenuncias([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var result = await _denunciaService.ListarMinhasDenunciasAsync(userId, page, pageSize);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<IEnumerable<DenunciaResponseDto>>> ListarTodas([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        try
        {
            var result = await _denunciaService.ListarTodasDenunciasAsync(page, pageSize);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{id}/status")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<DenunciaResponseDto>> AtualizarStatus(int id, [FromBody] string status)
    {
        try
        {
            var result = await _denunciaService.AtualizarStatusAsync(id, status);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
