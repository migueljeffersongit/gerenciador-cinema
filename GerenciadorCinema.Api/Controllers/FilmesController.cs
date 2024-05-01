using GerenciadorCinema.Application.Common;
using GerenciadorCinema.Application.DTOs.Filmes;
using GerenciadorCinema.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorCinema.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FilmesController : ControllerBase
{
    private readonly IFilmeService _filmeService;

    public FilmesController(IFilmeService filmeService)
    {
        _filmeService = filmeService;
    }

    [HttpGet]
    public async Task<PaginationResponse<FilmeResponseDto>> GetAllFilmes([FromQuery] GetListaFilmeQueryDto request)
    {
        return await _filmeService.GetListAsync(request, new CancellationToken());
    }

    [HttpGet("{id}")]
    public async Task<FilmeResponseDto> GetFilmeById(Guid id)
    {
        return await _filmeService.GetByIdAsync(id, new CancellationToken());
    }

    [HttpPost]
    public async Task<FilmeResponseDto> CreateFilme([FromBody] AddFilmeDto filme)
    {
        return await _filmeService.AddAsync(filme, new CancellationToken());
    }

    [HttpPut("{id}")]
    public async Task<FilmeResponseDto> UpdateFilme(Guid id, [FromBody] UpdateFilmeDto filme)
    {
        return await _filmeService.UpdateAsync(id, filme, new CancellationToken());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFilme(Guid id)
    {
        await _filmeService.DeleteAsync(id, new CancellationToken());
        return Ok();
    }
}
