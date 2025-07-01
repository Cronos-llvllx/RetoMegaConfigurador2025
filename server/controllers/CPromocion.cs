using megaapi.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace megaapi.controllers;

/// <summary>Controlador de la API para promociones.</summary>
/// <param name="repo">Inyección de dependencia del repositorio.</param>
[ApiController]
[Route("api/[controller]")]
public class Promocion(IPromocion repo) : ControllerBase
{
  private readonly IPromocion _repo = repo;

  // ** Definir endpoints...
  [HttpGet("")]
  public async Task<IActionResult> ObtenerPromociones()
  {
    return Ok(await _repo.ObtenerTodoAsync());
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> ObtenerPromocionPorId(int id)
  {
    try
    {
      var promocion = await _repo.ObtenerPorIdAsync(id)
        ?? throw new NullReferenceException();

      return Ok(promocion);
    }
    catch (NullReferenceException)
    {
      return NotFound($"No se encontró la promoción con el id {id}");
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex);
    }
  }
}