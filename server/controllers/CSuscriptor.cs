using megaapi.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace megaapi.controllers;

/// <summary>Controlador de la API para suscriptores.</summary>
/// <param name="repo">Inyección de dependencia del repositorio.</param>
[ApiController]
[Route("api/[controller]")]
public class CSuscriptor(ISuscriptor repo) : ControllerBase
{
  /// <summary>Repositorio de suscriptores</summary>
  public readonly ISuscriptor _repo = repo;

  // ** Definir endpoints...
  [HttpGet("hola")]
  public IActionResult HolaMundo()
  {
    return Ok("¡Hola mundo!");
  }
}