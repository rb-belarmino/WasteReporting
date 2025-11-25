using Microsoft.AspNetCore.Mvc;
using WasteReporting.API.DTOs;
using WasteReporting.API.Services;

namespace WasteReporting.API.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// Registra um novo usuário.
    /// </summary>
    /// <param name="dto">Dados do usuário (Nome, Email, Senha).</param>
    /// <returns>Retorna o token de acesso e dados do usuário criado.</returns>
    /// <response code="201">Usuário criado com sucesso.</response>
    /// <response code="400">Erro na requisição (ex: email já existe).</response>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuthResponseDto>> Register(RegisterDto dto)
    {
        try
        {
            var result = await _authService.RegisterAsync(dto);
            return CreatedAtAction(nameof(Register), result);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Realiza o login do usuário.
    /// </summary>
    /// <param name="dto">Credenciais (Email e Senha).</param>
    /// <returns>Retorna o token JWT para autenticação.</returns>
    /// <response code="200">Login realizado com sucesso.</response>
    /// <response code="401">Credenciais inválidas.</response>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthResponseDto>> Login(LoginDto dto)
    {
        try
        {
            var result = await _authService.LoginAsync(dto);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }
}
