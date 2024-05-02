using GerenciadorCinema.Api.Controllers;
using GerenciadorCinema.Application.Common;
using GerenciadorCinema.Application.DTOs.Salas;
using GerenciadorCinema.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorCinema.Tests;

public class SalasControllerTests
{
    private readonly Mock<ISalaService> _mockService;
    private readonly SalasController _controller;
    private readonly List<SalaResponseDto> _salas;

    public SalasControllerTests()
    {
        _mockService = new Mock<ISalaService>();
        _controller = new SalasController(_mockService.Object);
        _salas = new List<SalaResponseDto>
        {
            new SalaResponseDto { Id = Guid.Parse("99ef33b3-ac0f-4e96-8b61-a1faae89971b"), NumeroSala = "Sala 1", Descricao = "Sala principal com capacidade para 150 pessoas" },
            new SalaResponseDto { Id = Guid.Parse("9401bbb8-9499-4a9e-9475-2e61f16cb336"), NumeroSala = "Sala 2", Descricao = "Sala VIP com assentos reclináveis e serviço de bar" },
            new SalaResponseDto { Id = Guid.Parse("48d76a83-1453-4fef-ba32-a56110e12b7e"), NumeroSala = "Sala 3", Descricao = "Sala com projeção 3D" }
        };
    }

    [Fact]
    public async Task ObterTodasSalas_Retorna_RespostaPaginada()
    {
        var request = new GetListaSalaQueryDto();
        var expectedResponse = new PaginationResponse<SalaResponseDto>(_salas, _salas.Count, 1, _salas.Count);
        _mockService.Setup(s => s.GetListAsync(request, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(expectedResponse);

        var result = await _controller.GetAllSalas(request);

        Assert.Equal(expectedResponse.TotalCount, result.TotalCount);
        Assert.Equal(expectedResponse.Data.Count, result.Data.Count);
    }

    [Fact]
    public async Task ObterSalaPorId_Retorna_SalaResponseDto()
    {
        var salaId = Guid.Parse("99ef33b3-ac0f-4e96-8b61-a1faae89971b");
        var expectedSala = _salas[0];
        _mockService.Setup(s => s.GetByIdAsync(salaId, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(expectedSala);

        var result = await _controller.GetSalaById(salaId);

        Assert.Equal(expectedSala.Id, result.Id);
        Assert.Equal(expectedSala.NumeroSala, result.NumeroSala);
        Assert.Equal(expectedSala.Descricao, result.Descricao);
    }

    [Fact]
    public async Task CriarSala_Retorna_NovaSala()
    {
        var newSalaDto = new AddSalaDto { NumeroSala = "Sala 4", Descricao = "Nova sala com equipamentos modernos" };
        var expectedSala = new SalaResponseDto { Id = Guid.NewGuid(), NumeroSala = "Sala 4", Descricao = "Nova sala com equipamentos modernos" };
        _mockService.Setup(s => s.AddAsync(newSalaDto, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(expectedSala);

        var result = await _controller.CreateSala(newSalaDto);

        Assert.Equal(expectedSala.NumeroSala, result.NumeroSala);
        Assert.Equal(expectedSala.Descricao, result.Descricao);
    }

    [Fact]
    public async Task AtualizarSala_Retorna_SalaAtualizada()
    {
        var salaId = Guid.Parse("99ef33b3-ac0f-4e96-8b61-a1faae89971b");
        var updateSalaDto = new UpdateSalaDto { NumeroSala = "Sala 1", Descricao = "Sala principal atualizada" };
        var expectedSala = new SalaResponseDto { Id = salaId, NumeroSala = "Sala 1", Descricao = "Sala principal atualizada" };
        _mockService.Setup(s => s.UpdateAsync(salaId, updateSalaDto, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(expectedSala);

        var result = await _controller.UpdateSala(salaId, updateSalaDto);

        Assert.Equal(expectedSala.Descricao, result.Descricao);
    }

    [Fact]
    public async Task ExcluirSala_Retorna_ResultadoOk()
    {
        var salaId = Guid.Parse("99ef33b3-ac0f-4e96-8b61-a1faae89971b");
        _mockService.Setup(s => s.DeleteAsync(salaId, It.IsAny<CancellationToken>()))
                    .Returns(Task.CompletedTask);

        var result = await _controller.DeleteSala(salaId);

        Assert.IsType<OkResult>(result);
    }
}
