using GerenciadorCinema.Application.Services;
using GerenciadorCinema.Domain.Entities;
using GerenciadorCinema.Application.DTOs.Salas;
using GerenciadorCinema.Domain.Interfaces;
using GerenciadorCinema.Application.Common;
using GerenciadorCinema.Application.Interfaces.Queries;
using GerenciadorCinema.Domain.Interfaces.UoW;

namespace GerenciadorCinema.Tests;

public class SalaServiceTests
{
    private readonly Mock<ISalaQuery> _mockSalaQuery;
    private readonly Mock<ISalaRepository> _mockSalaRepository;
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly SalaService _salaService;

    public SalaServiceTests()
    {
        _mockSalaQuery = new Mock<ISalaQuery>();
        _mockSalaRepository = new Mock<ISalaRepository>();
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _salaService = new SalaService(_mockSalaQuery.Object, _mockSalaRepository.Object, _mockUnitOfWork.Object);
    }

    [Fact]
    public async Task ObterSalaPorId_Retorna_DetalhesDaSala()
    {
        var salaId = Guid.NewGuid();
        var expectedSala = new SalaResponseDto { Id = salaId, NumeroSala = "Sala 1", Descricao = "Descrição da Sala 1" };
        _mockSalaQuery.Setup(x => x.GetByIdAsync(salaId, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(expectedSala);

        var result = await _salaService.GetByIdAsync(salaId, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(salaId, result.Id);
    }

    [Fact]
    public async Task ObterListaSalas_Retorna_ListaPaginada()
    {
        var request = new GetListaSalaQueryDto();
        var expectedResponse = new PaginationResponse<SalaResponseDto>(new System.Collections.Generic.List<SalaResponseDto>(), 0, 1, 1);
        _mockSalaQuery.Setup(x => x.GetListAsync(request, It.IsAny<CancellationToken>()))
                       .ReturnsAsync(expectedResponse);

        var result = await _salaService.GetListAsync(request, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Empty(result.Data);
    }

    [Fact]
    public async Task AdicionarSala_CriaERetorna_Sala()
    {
        var salaDto = new AddSalaDto { NumeroSala = "Sala Nova", Descricao = "Nova Descrição" };
        var novaSala = new Sala(salaDto.NumeroSala, salaDto.Descricao);
        _mockSalaRepository.Setup(x => x.AddAsync(It.IsAny<Sala>(), new CancellationToken()));
        _mockUnitOfWork.Setup(x => x.CommitAsync(CancellationToken.None)).Returns(Task.CompletedTask);

        var result = await _salaService.AddAsync(salaDto, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(salaDto.NumeroSala, result.NumeroSala);
    }

    [Fact]
    public async Task AtualizarSala_AtualizaERetorna_SalaAtualizada()
    {
        var salaId = Guid.NewGuid();
        var salaDto = new UpdateSalaDto { NumeroSala = "Sala Atualizada", Descricao = "Descrição Atualizada" };
        var salaExistente = new Sala("Sala Antiga", "Descrição Antiga");
        _mockSalaRepository.Setup(x => x.GetByIdAsync(salaId, It.IsAny<CancellationToken>())).ReturnsAsync(salaExistente);
        _mockUnitOfWork.Setup(x => x.CommitAsync(CancellationToken.None)).Returns(Task.CompletedTask);

        var result = await _salaService.UpdateAsync(salaId, salaDto, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal(salaDto.NumeroSala, result.NumeroSala);
    }

    [Fact]
    public async Task ExcluirSala_ExcluiComSucesso()
    {
        var salaId = Guid.NewGuid();
        var salaExistente = new Sala("Sala para Excluir", "Descrição");
        _mockSalaRepository.Setup(x => x.GetByIdAsync(salaId, It.IsAny<CancellationToken>())).ReturnsAsync(salaExistente);
        _mockUnitOfWork.Setup(x => x.CommitAsync(CancellationToken.None)).Returns(Task.CompletedTask);

        await _salaService.DeleteAsync(salaId, CancellationToken.None);

        _mockSalaRepository.Verify(x => x.DeleteAsync(It.IsAny<Sala>(), new CancellationToken()), Times.Once());
    }
}
