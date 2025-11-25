using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WasteReporting.API.Controllers;
using WasteReporting.API.DTOs;
using WasteReporting.API.Services;
using Xunit;

namespace WasteReporting.Tests;

public class ReportsControllerTests
{
    private readonly Mock<IReportService> _serviceMock;
    private readonly ReportsController _controller;

    public ReportsControllerTests()
    {
        _serviceMock = new Mock<IReportService>();
        _controller = new ReportsController(_serviceMock.Object);

        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "1"),
            new Claim(ClaimTypes.Name, "testuser")
        }, "mock"));

        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user }
        };
    }

    [Fact]
    public async Task CreateReport_ShouldReturnCreated()
    {
        // Arrange
        var dto = new CreateReportDto { Location = "Loc", Description = "Desc" };
        var response = new ReportResponseDto { Id = 1, Location = "Loc", Description = "Desc" };

        _serviceMock.Setup(s => s.CreateReportAsync(dto, 1)).ReturnsAsync(response);

        // Act
        var result = await _controller.CreateReport(dto);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnedDto = Assert.IsType<ReportResponseDto>(actionResult.Value);
        Assert.Equal(1, returnedDto.Id);
    }

    [Fact]
    public async Task ListMyReports_ShouldReturnOk()
    {
        // Arrange
        var reports = new List<ReportResponseDto> { new ReportResponseDto { Id = 1 } };
        _serviceMock.Setup(s => s.ListMyReportsAsync(1, 1, 10)).ReturnsAsync(reports);

        // Act
        var result = await _controller.ListMyReports();

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnedList = Assert.IsAssignableFrom<IEnumerable<ReportResponseDto>>(actionResult.Value);
        Assert.Single(returnedList);
    }
}
