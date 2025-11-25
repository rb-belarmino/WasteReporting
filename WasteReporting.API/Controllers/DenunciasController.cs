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

    /// <summary>
    /// Cria uma nova denúncia.
    /// </summary>
    /// <remarks>
    /// Requer autenticação. A denúncia será vinculada ao usuário logado.
    /// </remarks>
    /// <param name="dto">Dados da denúncia (Localização e Descrição).</param>
    /// <returns>Retorna a denúncia criada.</returns>
    /// <response code="201">Denúncia criada com sucesso.</response>
    /// <response code="401">Não autorizado.</response>
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

    /// <summary>
    /// Lista as denúncias do usuário logado.
    /// </summary>
    /// <param name="page">Número da página (padrão: 1).</param>
    /// <param name="pageSize">Itens por página (padrão: 10).</param>
    /// <returns>Lista de denúncias.</returns>
    [HttpGet("minhas")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
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

    /// <summary>
    /// Lista todas as denúncias (Apenas Admin).
    /// </summary>
    /// <param name="page">Número da página (padrão: 1).</param>
    /// <param name="pageSize">Itens por página (padrão: 10).</param>
    /// <returns>Lista completa de denúncias.</returns>
    [HttpGet]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
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

    /// <summary>
    /// Atualiza o status de uma denúncia (Apenas Admin).
    /// </summary>
    /// <param name="id">ID da denúncia.</param>
    /// <param name="status">Novo status (ex: "RESOLVIDO").</param>
    /// <returns>A denúncia atualizada.</returns>
    [HttpPut("{id}/status")]
    [Authorize(Roles = "Admin")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
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
