using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WasteReporting.API.Controllers;
using WasteReporting.API.DTOs;
using WasteReporting.API.Services;
using Xunit;

namespace WasteReporting.Tests;

public class CollectionsControllerTests
{
    private readonly Mock<ICollectionService> _serviceMock;
    private readonly CollectionsController _controller;

    public CollectionsControllerTests()
    {
        _serviceMock = new Mock<ICollectionService>();
        _controller = new CollectionsController(_serviceMock.Object);
    }

    [Fact]
    public async Task Schedule_ShouldReturnCreated()
    {
        // Arrange
        var dto = new CreateCollectionDto { CollectionPointId = 1 };
        var response = new CollectionResponseDto { Id = 1 };

        _serviceMock.Setup(s => s.ScheduleCollectionAsync(dto)).ReturnsAsync(response);

        // Act
        var result = await _controller.Schedule(dto);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnedDto = Assert.IsType<CollectionResponseDto>(actionResult.Value);
        Assert.Equal(1, returnedDto.Id);
    }

    [Fact]
    public async Task GetById_ShouldReturnOk()
    {
        // Arrange
        var response = new CollectionResponseDto { Id = 1 };
        _serviceMock.Setup(s => s.GetCollectionByIdAsync(1)).ReturnsAsync(response);

        // Act
        var result = await _controller.GetById(1);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedDto = Assert.IsType<CollectionResponseDto>(actionResult.Value);
        Assert.Equal(1, returnedDto.Id);
    }

    [Fact]
    public async Task ListAll_ShouldReturnOk()
    {
        // Arrange
        var collections = new List<CollectionResponseDto> { new CollectionResponseDto { Id = 1 } };
        _serviceMock.Setup(s => s.ListCollectionsAsync(1, 10)).ReturnsAsync(collections);

        // Act
        var result = await _controller.ListAll(1, 10);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedList = Assert.IsAssignableFrom<IEnumerable<CollectionResponseDto>>(actionResult.Value);
        Assert.Single(returnedList);
    }

    [Fact]
    public async Task Update_ShouldReturnOk()
    {
        // Arrange
        var dto = new UpdateCollectionDto { CollectionPointId = 2 };
        var response = new CollectionResponseDto { Id = 1 };

        _serviceMock.Setup(s => s.UpdateCollectionAsync(1, dto)).ReturnsAsync(response);

        // Act
        var result = await _controller.Update(1, dto);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedDto = Assert.IsType<CollectionResponseDto>(actionResult.Value);
        Assert.Equal(1, returnedDto.Id);
    }

    [Fact]
    public async Task Delete_ShouldReturnNoContent()
    {
        // Arrange
        _serviceMock.Setup(s => s.DeleteCollectionAsync(1)).Returns(Task.CompletedTask);

        // Act
        var result = await _controller.Delete(1);

        // Assert
        Assert.IsType<NoContentResult>(result);
    }
}
