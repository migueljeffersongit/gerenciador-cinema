
using GerenciadorCinema.Application.Services;
using GerenciadorCinema.Domain.Entities;
using GerenciadorCinema.Application.DTOs.Filmes;
using GerenciadorCinema.Domain.Interfaces;
using GerenciadorCinema.Application.Common;
using GerenciadorCinema.Application.Interfaces.Queries;
using GerenciadorCinema.Domain.Interfaces.UoW;
using GerenciadorCinema.Application.Exceptions;

namespace GerenciadorCinema.Tests;

public class FilmeServiceTests
{
    private readonly Mock<IFilmeQuery> _mockFilmeQuery;
    private readonly Mock<IFilmeRepository> _mockFilmeRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly FilmeService _filmeService;

    public FilmeServiceTests()
    {
        _mockFilmeQuery = new Mock<IFilmeQuery>();
        _mockFilmeRepository = new Mock<IFilmeRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _filmeService = new FilmeService(_mockFilmeQuery.Object, _mockFilmeRepository.Object, _mockUnitOfWork.Object);
    }

    [Fact]
    public async Task ObterListaFilmes_Retorna_ListaPaginada()
    {
        var request = new GetListaFilmeQueryDto();
        var filmes = new List<FilmeResponseDto>
        {
            new FilmeResponseDto { Id = Guid.NewGuid(), Nome = "Forrest Gump", Diretor = "Robert Zemeckis", Duracao = new TimeSpan(2, 22, 0) }
        };
        var expectedResponse = new PaginationResponse<FilmeResponseDto>(filmes, 1, 1, 1);
        _mockFilmeQuery.Setup(x => x.GetListAsync(request, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(expectedResponse);

        var result = await _filmeService.GetListAsync(request, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Single(result.Data);
        Assert.Equal("Forrest Gump", result.Data[0].Nome);
    }

    [Fact]
    public async Task ObterFilmePorId_Retorna_DetalhesDoFilme()
    {
        var filmeId = Guid.NewGuid();
        var expectedFilme = new FilmeResponseDto { Id = filmeId, Nome = "Matrix", Diretor = "Lana e Lilly Wachowski", Duracao = new TimeSpan(2, 16, 0) };
        _mockFilmeQuery.Setup(x => x.GetByIdAsync(filmeId, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(expectedFilme);

        var result = await _filmeService.GetByIdAsync(filmeId, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(filmeId, result.Id);
    }

    [Fact]
    public async Task AdicionarFilme_CriaERetorna_Filme()
    {
        var filmeDto = new AddFilmeDto { Nome = "A Origem", Diretor = "Christopher Nolan", Duracao = new TimeSpan(2, 28, 0) };
        var filme = new Filme(filmeDto.Nome, filmeDto.Diretor, (TimeSpan)filmeDto.Duracao, filmeDto.SalaId);

        _mockFilmeRepository.Setup(x => x.AddAsync(It.IsAny<Filme>(), new CancellationToken()));
        _mockUnitOfWork.Setup(x => x.CommitAsync(CancellationToken.None)).Returns(Task.CompletedTask);

        var result = await _filmeService.AddAsync(filmeDto, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal("A Origem", result.Nome);
    }

    [Fact]
    public async Task AtualizarFilme_AtualizaERetorna_FilmeAtualizado()
    {
        var filmeId = Guid.NewGuid();
        var filmeDto = new UpdateFilmeDto { Nome = "Matrix Atualizado", Diretor = "Lana e Lilly Wachowski", Duracao = new TimeSpan(2, 30, 0) };
        var existingFilme = new Filme("Matrix", "Lana e Lilly Wachowski", new TimeSpan(2, 16, 0), null);

        _mockFilmeRepository.Setup(x => x.GetByIdAsync(filmeId, It.IsAny<CancellationToken>())).ReturnsAsync(existingFilme);
        _mockFilmeRepository.Setup(x => x.UpdateAsync(It.IsAny<Filme>(), new CancellationToken()));
        _mockUnitOfWork.Setup(x => x.CommitAsync(CancellationToken.None)).Returns(Task.CompletedTask);

        var result = await _filmeService.UpdateAsync(filmeId, filmeDto, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal("Matrix Atualizado", result.Nome);
    }

    [Fact]
    public async Task ExcluirFilme_ExcluiFilme()
    {
        var filmeId = Guid.NewGuid();
        var existingFilme = new Filme("Matrix", "Lana e Lilly Wachowski", new TimeSpan(2, 16, 0), null);

        _mockFilmeRepository.Setup(x => x.GetByIdAsync(filmeId, It.IsAny<CancellationToken>())).ReturnsAsync(existingFilme);
        _mockFilmeRepository.Setup(x => x.DeleteAsync(It.IsAny<Filme>(), new CancellationToken()));
        _mockUnitOfWork.Setup(x => x.CommitAsync(CancellationToken.None)).Returns(Task.CompletedTask);

        await _filmeService.DeleteAsync(filmeId, CancellationToken.None);

        _mockFilmeRepository.Verify(x => x.DeleteAsync(It.IsAny<Filme>(), new CancellationToken()), Times.Once());
    }

    [Fact]
    public async Task AtualizarSalaIdAsync_FilmeNaoEncontrado_ThrowsNotFoundException()
    {
        var filmeId = Guid.NewGuid();
        _mockFilmeRepository.Setup(repo => repo.GetByIdAsync(filmeId, It.IsAny<CancellationToken>())).ReturnsAsync((Filme)null);

        await Assert.ThrowsAsync<NotFoundException>(() => _filmeService.UpdateSalaIdAsync(filmeId, Guid.NewGuid(), new CancellationToken()));
    }

    [Fact]
    public async Task AtualizarSalaIdAsync_SalaNaoEncontrada_ThrowsNotFoundException()
    {
        var filmeId = Guid.NewGuid();
        var salaId = Guid.NewGuid();
        var filme = new Filme("Matrix", "Wachowski", TimeSpan.FromHours(2), salaId);
        
    _mockFilmeRepository.Setup(repo => repo.GetByIdAsync(filmeId, It.IsAny<CancellationToken>())).ReturnsAsync(filme);
        _mockFilmeQuery.Setup(query => query.ExisteSala(salaId, It.IsAny<CancellationToken>())).ReturnsAsync(false);

        await Assert.ThrowsAsync<NotFoundException>(() => _filmeService.UpdateSalaIdAsync(filmeId, salaId, new CancellationToken()));
    }

    [Fact]
    public async Task AtualizarSalaIdAsync_Sucesso_RetornaTrue()
    {
        var filmeId = Guid.NewGuid();
        var salaId = Guid.NewGuid();
        var filme = new Filme("Matrix", "Wachowski", TimeSpan.FromHours(2), salaId);

        _mockFilmeRepository.Setup(repo => repo.GetByIdAsync(filmeId, It.IsAny<CancellationToken>())).ReturnsAsync(filme);
        _mockFilmeQuery.Setup(query => query.ExisteSala(salaId, It.IsAny<CancellationToken>())).ReturnsAsync(true);
        _mockFilmeRepository.Setup(repo => repo.UpdateAsync(filme, new CancellationToken()));
        _mockUnitOfWork.Setup(uow => uow.CommitAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask);

        var result = await _filmeService.UpdateSalaIdAsync(filmeId, salaId, new CancellationToken());

        Assert.True(result);
    }

}
