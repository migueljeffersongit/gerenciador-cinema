using GerenciadorCinema.Api.Filters;
using GerenciadorCinema.Application.Common;
using GerenciadorCinema.Application.DTOs.Filmes;
using GerenciadorCinema.Application.Exceptions;
using GerenciadorCinema.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorCinema.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[NullResponseFilter]
public class FilmesController : ControllerBase
{
    private readonly IFilmeService _filmeService;

    public FilmesController(IFilmeService filmeService)
    {
        _filmeService = filmeService;
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<PaginationResponse<FilmeResponseDto>> GetAllFilmes([FromQuery] GetListaFilmeQueryDto request)
    {
        return await _filmeService.GetListAsync(request, new CancellationToken());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<FilmeResponseDto> GetFilmeById(Guid id)
    {
        return await _filmeService.GetByIdAsync(id, new CancellationToken());
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<FilmeResponseDto> CreateFilme([FromBody] AddFilmeDto filme)
    {
        return await _filmeService.AddAsync(filme, new CancellationToken());
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<FilmeResponseDto> UpdateFilme(Guid id, [FromBody] UpdateFilmeDto filme)
    {
        return await _filmeService.UpdateAsync(id, filme, new CancellationToken());
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteFilme(Guid id)
    {
        await _filmeService.DeleteAsync(id, new CancellationToken());
        return Ok();
    }

    [HttpPatch("{id}/sala")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> UpdateSalaId(Guid id, [FromBody] Guid? novaSalaId)
    {
        var resultado = await _filmeService.UpdateSalaIdAsync(id, novaSalaId, new CancellationToken());
        if (resultado)
            return Ok();
        return BadRequest("Nao foi possivel alterar a sala do filme.");
    }

}
