using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WasteReporting.API.Controllers;
using WasteReporting.API.ViewModels;
using WasteReporting.API.Services;
using Xunit;

namespace WasteReporting.Tests;

public class CollectionPointsControllerTests
{
    private readonly Mock<IManagementService> _serviceMock;
    private readonly CollectionPointsController _controller;

    public CollectionPointsControllerTests()
    {
        _serviceMock = new Mock<IManagementService>();
        _controller = new CollectionPointsController(_serviceMock.Object);
    }

    [Fact]
    public async Task Create_ShouldReturnCreated()
    {
        // Arrange
        var dto = new CreateCollectionPointViewModel { Location = "Loc", Responsible = "Resp" };
        var response = new CollectionPointViewModel { Id = 1, Location = "Loc" };

        _serviceMock.Setup(s => s.CreateCollectionPointAsync(dto)).ReturnsAsync(response);

        // Act
        var result = await _controller.Create(dto);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnedViewModel = Assert.IsType<CollectionPointViewModel>(actionResult.Value);
        Assert.Equal(1, returnedViewModel.Id);
        Assert.Equal("Loc", returnedViewModel.Location);
    }
}
