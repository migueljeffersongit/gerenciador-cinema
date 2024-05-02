using GerenciadorCinema.Api.Controllers;
using GerenciadorCinema.Application.Common;
using GerenciadorCinema.Application.DTOs.Filmes;
using GerenciadorCinema.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorCinema.Tests;

public class FilmesControllerTests
{
    private readonly Mock<IFilmeService> _mockService;
    private readonly FilmesController _controller;
    private readonly List<FilmeResponseDto> _filmes;


    public FilmesControllerTests()
    {
        _mockService = new Mock<IFilmeService>();
        _controller = new FilmesController(_mockService.Object);
        _filmes = new List<FilmeResponseDto>
        {
            new FilmeResponseDto
            {
                Id = Guid.NewGuid(),
                Nome = "Forrest Gump",
                Diretor = "Robert Zemeckis",
                Duracao = new TimeSpan(2, 22, 0),
                SalaId = null
            },
            new FilmeResponseDto
            {
                Id = Guid.NewGuid(),
                Nome = "Matrix",
                Diretor = "Lana e Lilly Wachowski",
                Duracao = new TimeSpan(2, 16, 0),
                SalaId = Guid.Parse("9401bbb8-9499-4a9e-9475-2e61f16cb336")
            },
            new FilmeResponseDto
            {
                Id = Guid.NewGuid(),
                Nome = "A Origem",
                Diretor = "Christopher Nolan",
                Duracao = new TimeSpan(2, 28, 0),
                SalaId = null
            }
        };
    }

    [Fact]
    public async Task ObterTodosFilmes_Retorna_RespostaPaginada()
    {
        var request = new GetListaFilmeQueryDto();

        var expectedResponse = new PaginationResponse<FilmeResponseDto>(_filmes, _filmes.Count(), 1, _filmes.Count());
        _mockService.Setup(s => s.GetListAsync(request, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(expectedResponse);

        var result = await _controller.GetAllFilmes(request);

        Assert.Equal(expectedResponse.Data.Count, result.Data.Count);
        Assert.Equal(expectedResponse.TotalCount, result.TotalCount);
        Assert.Equal(expectedResponse.PageSize, result.PageSize);
    }

    [Fact]
    public async Task ObterFilmePorId_Retorna_FilmeResponseDto()
    {
        var filmeId = Guid.Parse("d9c2a815-3550-4726-bd8e-e2c5d77d8cf9");
        var expectedFilme = new FilmeResponseDto
        {
            Id = Guid.Parse("d9c2a815-3550-4726-bd8e-e2c5d77d8cf9"),
            Nome = "Matrix",
            Diretor = "Lana e Lilly Wachowski",
            Duracao = new TimeSpan(2, 16, 0),
            SalaId = Guid.Parse("9401bbb8-9499-4a9e-9475-2e61f16cb336")
        };
        _mockService.Setup(s => s.GetByIdAsync(filmeId, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(expectedFilme);

        var result = await _controller.GetFilmeById(filmeId);

        Assert.Equal(expectedFilme.Id, result.Id);
    }

    [Fact]
    public async Task CriarFilme_Retorna_FilmeCriado()
    {
        var filmeDto = new AddFilmeDto
        {
            Nome = "Matrix",
            Diretor = "Lana e Lilly Wachowski",
            Duracao = new TimeSpan(2, 16, 0),
            SalaId = Guid.Parse("9401bbb8-9499-4a9e-9475-2e61f16cb336")
        };
        var expectedFilme = new FilmeResponseDto
        {
            Id = Guid.Parse("d9c2a815-3550-4726-bd8e-e2c5d77d8cf9"),
            Nome = "Matrix",
            Diretor = "Lana e Lilly Wachowski",
            Duracao = new TimeSpan(2, 16, 0),
            SalaId = Guid.Parse("9401bbb8-9499-4a9e-9475-2e61f16cb336")
        };
        _mockService.Setup(s => s.AddAsync(filmeDto, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(expectedFilme);

        var result = await _controller.CreateFilme(filmeDto);

        Assert.Equal(expectedFilme, result);
    }

    [Fact]
    public async Task AtualizarFilme_Retorna_FilmeAtualizado()
    {
        var filmeId = Guid.NewGuid();
        var filmeDto = new UpdateFilmeDto
        {
            Nome = "Matrix",
            Diretor = "Lana e Lilly Wachowski",
            Duracao = new TimeSpan(2, 16, 0),
            SalaId = Guid.Parse("9401bbb8-9499-4a9e-9475-2e61f16cb336")
        };
        var expectedFilme = new FilmeResponseDto
        {
            Id = Guid.Parse("d9c2a815-3550-4726-bd8e-e2c5d77d8cf9"),
            Nome = "Matrix",
            Diretor = "Lana e Lilly Wachowski",
            Duracao = new TimeSpan(2, 16, 0),
            SalaId = Guid.Parse("9401bbb8-9499-4a9e-9475-2e61f16cb336")
        };
        _mockService.Setup(s => s.UpdateAsync(filmeId, filmeDto, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(expectedFilme);

        var result = await _controller.UpdateFilme(filmeId, filmeDto);

        Assert.Equal(expectedFilme, result);
    }

    [Fact]
    public async Task ExcluirFilme_Retorna_ResultadoOk()
    {
        var filmeId = Guid.Parse("d9c2a815-3550-4726-bd8e-e2c5d77d8cf9");

        _mockService.Setup(s => s.DeleteAsync(filmeId, It.IsAny<CancellationToken>()))
                    .Returns(Task.CompletedTask);

        var result = await _controller.DeleteFilme(filmeId);

        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task AlterarSalaId_RetornoOk_WhenSuccessful()
    {
        var filmeId = Guid.NewGuid();
        var novaSalaId = Guid.NewGuid();
        _mockService.Setup(service => service.UpdateSalaIdAsync(filmeId, novaSalaId, It.IsAny<CancellationToken>())).ReturnsAsync(true);

        var result = await _controller.UpdateSalaId(filmeId, novaSalaId);

        Assert.IsType<OkResult>(result);
    }

    [Fact]
    public async Task AlterarSalaId_RetornaBadRequest_WhenFailed()
    {
        var filmeId = Guid.NewGuid();
        var novaSalaId = Guid.NewGuid();
        _mockService.Setup(service => service.UpdateSalaIdAsync(filmeId, novaSalaId, It.IsAny<CancellationToken>())).ReturnsAsync(false);

        var result = await _controller.UpdateSalaId(filmeId, novaSalaId);

        Assert.IsType<BadRequestObjectResult>(result);
        var badRequestResult = result as BadRequestObjectResult;
        Assert.Equal("Nao foi possivel alterar a sala do filme.", badRequestResult.Value);
    }

}

