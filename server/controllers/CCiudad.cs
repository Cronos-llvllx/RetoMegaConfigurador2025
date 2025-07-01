using megaapi.interfaces;
using megaapi.models.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace megaapi.controllers;

/// <summary>Controlador de la API para ciudades.</summary>
/// <param name="repo">Inyección de dependencia del repositorio.</param>
[ApiController]
[Route("api/[controller]")]
public class Ciudad(ICiudad repo) : ControllerBase
{
  /// <summary>Repositorio de ciudades.</summary>
  private readonly ICiudad _repo = repo;

  /// <summary>Obtiene todas las ciudades.</summary>
  /// <returns>Lista de ciudades simplificadas.</returns>
  [HttpGet]
  public async Task<IActionResult> ObtenerTodas()
  {
    try
    {
      var ciudades = await _repo.ObtenerTodoAsync();
      var ciudadesDto = ciudades.Select(c => new CiudadDto 
      {
        Id = c.Idciudad,
        Name = c.Nombre
      });
      return Ok(ciudadesDto);
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }

  /// <summary>Obtiene una ciudad por su ID.</summary>
  /// <param name="id">ID de la ciudad.</param>
  /// <returns>Ciudad encontrada simplificada.</returns>
  [HttpGet("{id}")]
  public async Task<IActionResult> ObtenerPorId(int id)
  {
    try
    {
      var ciudad = await _repo.ObtenerPorIdAsync(id);
      if (ciudad == null)
        return NotFound($"Ciudad con ID {id} no encontrada.");
      
      var ciudadDto = new CiudadDto
      {
        Id = ciudad.Idciudad,
        Name = ciudad.Nombre
      };
      
      return Ok(ciudadDto);
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }
}