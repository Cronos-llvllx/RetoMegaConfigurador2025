using megaapi.interfaces;
using megaapi.models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace megaapi.controllers;

/// <summary>Controlador de la API para colonias.</summary>
/// <param name="repo">Inyección de dependencia del repositorio.</param>
[ApiController]
[Route("api/[controller]")]
public class Colonia(IColonia repo) : ControllerBase
{
  private readonly IColonia _repo = repo;

  /// <summary>Obtiene todas las colonias.</summary>
  /// <returns>Lista de colonias simplificadas.</returns>
  [HttpGet]
  public async Task<IActionResult> ObtenerTodas()
  {
    try
    {
      var colonias = await _repo.ObtenerTodoAsync();
      var coloniasDto = colonias.Select(c => new ColoniaDto 
      {
        Id = c.Idcolonia,
        Name = c.Nombre,
        CiudadId = c.Idciudad
      });
      return Ok(coloniasDto);
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }

  /// <summary>Obtiene una colonia por su ID.</summary>
  /// <param name="id">ID de la colonia.</param>
  /// <returns>Colonia encontrada simplificada.</returns>
  [HttpGet("{id}")]
  public async Task<IActionResult> ObtenerPorId(int id)
  {
    try
    {
      var colonia = await _repo.ObtenerPorIdAsync(id);
      if (colonia == null)
        return NotFound($"Colonia con ID {id} no encontrada.");
      
      var coloniaDto = new ColoniaDto
      {
        Id = colonia.Idcolonia,
        Name = colonia.Nombre,
        CiudadId = colonia.Idciudad
      };
      
      return Ok(coloniaDto);
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }

  /// <summary>Obtiene todas las colonias de una ciudad específica.</summary>
  /// <param name="ciudadId">ID de la ciudad.</param>
  /// <returns>Lista de colonias de la ciudad simplificadas.</returns>
  [HttpGet("ciudad/{ciudadId}")]
  public async Task<IActionResult> ObtenerPorCiudad(int ciudadId)
  {
    try
    {
      var colonias = await _repo.ObtenerPorCiudadAsync(ciudadId);
      var coloniasDto = colonias.Select(c => new ColoniaDto
      {
        Id = c.Idcolonia,
        Name = c.Nombre,
        CiudadId = c.Idciudad
      });
      return Ok(coloniasDto);
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }
}