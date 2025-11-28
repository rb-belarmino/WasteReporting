using Microsoft.AspNetCore.Mvc;
using Moq;
using WasteReporting.API.Controllers;
using WasteReporting.API.ViewModels;
using WasteReporting.API.Services;
using Xunit;

namespace WasteReporting.Tests;

public class RecyclersControllerTests
{
    private readonly Mock<IManagementService> _serviceMock;
    private readonly RecyclersController _controller;

    public RecyclersControllerTests()
    {
        _serviceMock = new Mock<IManagementService>();
        _controller = new RecyclersController(_serviceMock.Object);
    }

    [Fact]
    public async Task Create_ShouldReturnCreated()
    {
        // Arrange
        var dto = new CreateRecyclerViewModel { Name = "Name", Category = "Cat" };
        var response = new RecyclerViewModel { Id = 1, Name = "Name" };

        _serviceMock.Setup(s => s.CreateRecyclerAsync(dto)).ReturnsAsync(response);

        // Act
        var result = await _controller.Create(dto);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnedViewModel = Assert.IsType<RecyclerViewModel>(actionResult.Value);
        Assert.Equal(1, returnedViewModel.Id);
        Assert.Equal("Name", returnedViewModel.Name);
    }
}
