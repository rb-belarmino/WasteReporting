using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WasteReporting.API.Controllers;
using WasteReporting.API.DTOs;
using WasteReporting.API.Services;
using Xunit;

namespace WasteReporting.Tests;

public class DenunciasControllerTests
{
    private readonly Mock<IDenunciaService> _mockDenunciaService;
    private readonly DenunciasController _controller;

    public DenunciasControllerTests()
    {
        _mockDenunciaService = new Mock<IDenunciaService>();
        _controller = new DenunciasController(_mockDenunciaService.Object);

        // Mock User Claims
        var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.NameIdentifier, "1"),
        }, "mock"));

        _controller.ControllerContext = new ControllerContext()
        {
            HttpContext = new DefaultHttpContext() { User = user }
        };
    }

    [Fact]
    public async Task CriarDenuncia_ReturnsCreated_WhenSuccess()
    {
        // Arrange
        var createDto = new CreateDenunciaDto { Localizacao = "Rua A", Descricao = "Lixo" };
        var responseDto = new DenunciaResponseDto { Id = 1, Localizacao = "Rua A", Status = "PENDENTE" };

        _mockDenunciaService.Setup(s => s.CriarDenunciaAsync(createDto, 1)).ReturnsAsync(responseDto);

        // Act
        var result = await _controller.CriarDenuncia(createDto);

        // Assert
        var actionResult = Assert.IsType<CreatedAtActionResult>(result.Result);
        var returnValue = Assert.IsType<DenunciaResponseDto>(actionResult.Value);
        Assert.Equal(1, returnValue.Id);
    }

    [Fact]
    public async Task ListarMinhasDenuncias_ReturnsOk_WhenSuccess()
    {
        // Arrange
        var list = new List<DenunciaResponseDto> { new DenunciaResponseDto { Id = 1 } };
        _mockDenunciaService.Setup(s => s.ListarMinhasDenunciasAsync(1, 1, 10)).ReturnsAsync(list);

        // Act
        var result = await _controller.ListarMinhasDenuncias(1, 10);

        // Assert
        var actionResult = Assert.IsType<OkObjectResult>(result.Result);
        var returnValue = Assert.IsAssignableFrom<IEnumerable<DenunciaResponseDto>>(actionResult.Value);
        Assert.Single(returnValue);
    }
}
