using GerenciadorCinema.Api.Filters;
using GerenciadorCinema.Application.Common;
using GerenciadorCinema.Application.DTOs.Salas;
using GerenciadorCinema.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace GerenciadorCinema.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[NullResponseFilter]
public class SalasController : ControllerBase
{
    private readonly ISalaService _salaService;

    public SalasController(ISalaService salaService)
    {
        _salaService = salaService;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<PaginationResponse<SalaResponseDto>> GetAllSalas([FromQuery] GetListaSalaQueryDto request)
    {
        return await _salaService.GetListAsync(request, new CancellationToken());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<SalaResponseDto> GetSalaById(Guid id)
    {
        return await _salaService.GetByIdAsync(id, new CancellationToken());
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]    
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<SalaResponseDto> CreateSala([FromBody] AddSalaDto sala)
    {
       return await _salaService.AddAsync(sala, new CancellationToken());
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<SalaResponseDto> UpdateSala(Guid id, [FromBody] UpdateSalaDto sala)
    {
       return await _salaService.UpdateAsync(id, sala, new CancellationToken());
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> DeleteSala(Guid id)
    {
        await _salaService.DeleteAsync(id, new CancellationToken());
        return Ok();
    }
}
