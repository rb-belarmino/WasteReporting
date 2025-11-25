using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WasteReporting.API.Controllers;
using WasteReporting.API.DTOs;
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
        var dto = new CreateCollectionPointDto { Location = "Loc", Responsible = "Resp" };
        var response = new CollectionPointDto { Id = 1, Location = "Loc" };

        _serviceMock.Setup(s => s.CreateCollectionPointAsync(dto)).ReturnsAsync(response);

        // Act
        var result = await _controller.Create(dto);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnedDto = Assert.IsType<CollectionPointDto>(actionResult.Value);
        Assert.Equal(1, returnedDto.Id);
        Assert.Equal("Loc", returnedDto.Location);
    }
}
