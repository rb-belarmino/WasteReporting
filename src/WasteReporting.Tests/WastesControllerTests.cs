using Microsoft.AspNetCore.Mvc;
using Moq;
using WasteReporting.API.Controllers;
using WasteReporting.API.ViewModels;
using WasteReporting.API.Services;
using Xunit;

namespace WasteReporting.Tests;

public class WastesControllerTests
{
    private readonly Mock<IManagementService> _serviceMock;
    private readonly WastesController _controller;

    public WastesControllerTests()
    {
        _serviceMock = new Mock<IManagementService>();
        _controller = new WastesController(_serviceMock.Object);
    }

    [Fact]
    public async Task Create_ShouldReturnCreated()
    {
        // Arrange
        var dto = new CreateWasteViewModel { Type = "Type" };
        var response = new WasteViewModel { Id = 1, Type = "Type" };

        _serviceMock.Setup(s => s.CreateWasteAsync(dto)).ReturnsAsync(response);

        // Act
        var result = await _controller.Create(dto);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnedViewModel = Assert.IsType<WasteViewModel>(actionResult.Value);
        Assert.Equal(1, returnedViewModel.Id);
    }

    [Fact]
    public async Task ListAll_ShouldReturnOk()
    {
        // Arrange
        var wastes = new List<WasteViewModel> { new WasteViewModel { Id = 1, Type = "Type" } };
        _serviceMock.Setup(s => s.ListWastesAsync(1, 10)).ReturnsAsync(wastes);

        // Act
        var result = await _controller.ListAll(1, 10);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedList = Assert.IsAssignableFrom<IEnumerable<WasteViewModel>>(actionResult.Value);
        Assert.Single(returnedList);
    }
}
