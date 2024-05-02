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
    public async Task<PaginationResponse<SalaResponseDto>> GetAllSalas([FromQuery] GetListaSalaQueryDto request)
    {
        return await _salaService.GetListAsync(request, new CancellationToken());
    }

    [HttpGet("{id}")]
    public async Task<SalaResponseDto> GetSalaById(Guid id)
    {
        return await _salaService.GetByIdAsync(id, new CancellationToken());
    }

    [HttpPost]
    public async Task<SalaResponseDto> CreateSala([FromBody] AddSalaDto sala)
    {
       return await _salaService.AddAsync(sala, new CancellationToken());
    }

    [HttpPut("{id}")]
    public async Task<SalaResponseDto> UpdateSala(Guid id, [FromBody] UpdateSalaDto sala)
    {
       return await _salaService.UpdateAsync(id, sala, new CancellationToken());
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSala(Guid id)
    {
        await _salaService.DeleteAsync(id, new CancellationToken());
        return Ok();
    }
}
