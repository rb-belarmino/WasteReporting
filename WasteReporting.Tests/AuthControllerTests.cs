using Microsoft.AspNetCore.Mvc;
using Moq;
using WasteReporting.API.Controllers;
using WasteReporting.API.ViewModels;
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
        var registerViewModel = new RegisterViewModel { Username = "test", Email = "test@test.com", Password = "123" };
        var responseViewModel = new AuthResponseViewModel { Token = "token", Username = "test" };

        _mockAuthService.Setup(s => s.RegisterAsync(registerViewModel)).ReturnsAsync(responseViewModel);

        // Act
        var result = await _controller.Register(registerViewModel);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnValue = Assert.IsType<AuthResponseViewModel>(actionResult.Value);
        Assert.Equal("test", returnValue.Username);
    }

    [Fact]
    public async Task Login_ReturnsOk_WhenSuccess()
    {
        // Arrange
        var loginViewModel = new LoginViewModel { Email = "test@test.com", Password = "123" };
        var responseViewModel = new AuthResponseViewModel { Token = "token", Username = "test" };

        _mockAuthService.Setup(s => s.LoginAsync(loginViewModel)).ReturnsAsync(responseViewModel);

        // Act
        var result = await _controller.Login(loginViewModel);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsType<AuthResponseViewModel>(actionResult.Value);
        Assert.Equal("token", returnValue.Token);
    }
}
