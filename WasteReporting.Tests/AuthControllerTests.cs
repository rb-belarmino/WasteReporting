using Microsoft.AspNetCore.Mvc;
using Moq;
using WasteReporting.API.Controllers;
using WasteReporting.API.DTOs;
using WasteReporting.API.Services;
using Xunit;

namespace WasteReporting.Tests;

public class AuthControllerTests
{
    private readonly Mock<IAuthService> _mockAuthService;
    private readonly AuthController _controller;

    public AuthControllerTests()
    {
        _mockAuthService = new Mock<IAuthService>();
        _controller = new AuthController(_mockAuthService.Object);
    }

    [Fact]
    public async Task Register_ReturnsCreated_WhenSuccess()
    {
        // Arrange
        var registerDto = new RegisterDto { Username = "test", Email = "test@test.com", Password = "123" };
        var responseDto = new AuthResponseDto { Token = "token", Username = "test" };

        _mockAuthService.Setup(s => s.RegisterAsync(registerDto)).ReturnsAsync(responseDto);

        // Act
        var result = await _controller.Register(registerDto);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnValue = Assert.IsType<AuthResponseDto>(actionResult.Value);
        Assert.Equal("test", returnValue.Username);
    }

    [Fact]
    public async Task Login_ReturnsOk_WhenSuccess()
    {
        // Arrange
        var loginDto = new LoginDto { Email = "test@test.com", Password = "123" };
        var responseDto = new AuthResponseDto { Token = "token", Username = "test" };

        _mockAuthService.Setup(s => s.LoginAsync(loginDto)).ReturnsAsync(responseDto);

        // Act
        var result = await _controller.Login(loginDto);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<AuthResponseDto>(actionResult.Value);
        Assert.Equal("token", returnValue.Token);
    }
}
