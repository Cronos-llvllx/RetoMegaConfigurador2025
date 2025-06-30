using megaapi.interfaces;
using megaapi.models; // Asegúrate de tener este using
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks; // Y este también

namespace megaapi.controllers;

/// <summary>Controlador de la API para contratos.</summary>
/// <param name="repo">Inyección de dependencia del repositorio.</param>
[ApiController]
[Route("api/[controller]")]
public class Contrato(IContrato repo) : ControllerBase
{
  private readonly IContrato _repo = repo;

  // ** Definir endpoints.
  [HttpGet("")]
  public async Task<IActionResult> ObtenerContratos()
  {
    return Ok(await _repo.ObtenerTodoAsync());
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> ObtenerContratoPorId(int id)
  {
    var contrato = await _repo.ObtenerPorIdAsync(id);

    if (contrato == null)
      return NotFound($"No se encontró un contrato con el identificador {id}");

    return Ok(contrato);
  }
}