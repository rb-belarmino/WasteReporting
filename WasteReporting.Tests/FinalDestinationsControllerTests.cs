using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WasteReporting.API.Controllers;
using WasteReporting.API.DTOs;
using WasteReporting.API.Services;
using Xunit;

namespace WasteReporting.Tests;

public class FinalDestinationsControllerTests
{
    private readonly Mock<IManagementService> _serviceMock;
    private readonly FinalDestinationsController _controller;

    public FinalDestinationsControllerTests()
    {
        _serviceMock = new Mock<IManagementService>();
        _controller = new FinalDestinationsController(_serviceMock.Object);
    }

    [Fact]
    public async Task Create_ShouldReturnCreated()
    {
        // Arrange
        var dto = new CreateFinalDestinationDto { Description = "Desc" };
        var response = new FinalDestinationDto { Id = 1, Description = "Desc" };

        _serviceMock.Setup(s => s.CreateFinalDestinationAsync(dto)).ReturnsAsync(response);

        // Act
        var result = await _controller.Create(dto);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnedDto = Assert.IsType<FinalDestinationDto>(actionResult.Value);
        Assert.Equal(1, returnedDto.Id);
    }
}
