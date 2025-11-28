using Microsoft.AspNetCore.Mvc;
using WasteReporting.API.ViewModels;
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
    /// Registers a new user.
    /// </summary>
    /// <param name="dto">User data (Name, Email, Password).</param>
    /// <returns>Returns the access token and created user data.</returns>
    /// <response code="201">User created successfully.</response>
    /// <response code="400">Request error (e.g., email already exists).</response>
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<AuthResponseViewModel>> Register(RegisterViewModel dto)
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
    /// Logs in the user.
    /// </summary>
    /// <param name="dto">Credentials (Email and Password).</param>
    /// <returns>Returns the JWT token for authentication.</returns>
    /// <response code="200">Login successful.</response>
    /// <response code="401">Invalid credentials.</response>
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<AuthResponseViewModel>> Login(LoginViewModel dto)
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
