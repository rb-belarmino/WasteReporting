using Microsoft.AspNetCore.Mvc;
using Moq;
using WasteReporting.API.Controllers;
using WasteReporting.API.DTOs;
using WasteReporting.API.Services;
using Xunit;

namespace WasteReporting.Tests;

public class CollectionWastesControllerTests
{
    private readonly Mock<ICollectionService> _serviceMock;
    private readonly CollectionWastesController _controller;

    public CollectionWastesControllerTests()
    {
        _serviceMock = new Mock<ICollectionService>();
        _controller = new CollectionWastesController(_serviceMock.Object);
    }

    [Fact]
    public async Task Associate_ShouldReturnOk()
    {
        // Arrange
        var dto = new CreateCollectionWasteDto { CollectionId = 1, WasteId = 1, WeightKg = 10 };
        _serviceMock.Setup(s => s.AssociateWasteAsync(dto)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Associate(dto);

        // Assert
        Assert.IsType<OkObjectResult>(result);
    }

    [Fact]
    public async Task Disassociate_ShouldReturnNoContent()
    {
        // Arrange
        _serviceMock.Setup(s => s.DisassociateWasteAsync(1, 1)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Disassociate(1, 1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }

    [Fact]
    public async Task ListAll_ShouldReturnOk()
    {
        // Arrange
        var list = new List<CollectionWasteResponseDto> { new CollectionWasteResponseDto { CollectionId = 1, WasteId = 1 } };
        _serviceMock.Setup(s => s.ListAssociationsAsync()).ReturnsAsync(list);

        // Act
        var result = await _controller.ListAll();

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedList = Assert.IsAssignableFrom<IEnumerable<CollectionWasteResponseDto>>(actionResult.Value);
        Assert.Single(returnedList);
    }
}
