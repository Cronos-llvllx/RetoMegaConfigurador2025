using megaapi.interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

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

  [HttpGet("")]
  public async Task<IActionResult> ObtenerSuscriptores()
  {
    return Ok(await _repo.GetAllAsync());
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> ObtenerSuscriptorPorId(int id)
  {
    try
    {
      var suscriptor = (await _repo.GetAllAsync()).FirstOrDefault(s => s.Idsuscriptor == id);

      if (suscriptor != null)
        return Ok(suscriptor);

      throw new NullReferenceException($"No existe el suscriptor con id {id}");
    }
    catch (SqlException ex)
    {
      Console.Error.WriteLine(ex);
      return BadRequest(ex.Message);
    }
    catch (NullReferenceException ex)
    {
      Console.Error.WriteLine(ex);
      return NotFound(ex.Message);
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine(ex);
      return StatusCode(500, ex.Message);
    }
  }
}