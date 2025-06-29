using megaapi.interfaces;
using megaapi.models; // Asegúrate de tener este using
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks; // Y este también

namespace megaapi.controllers;

/// <summary>Controlador de la API para contratos.</summary>
/// <param name="repo">Inyección de dependencia del repositorio.</param>
[ApiController]
[Route("api/[controller]")]
public class CContrato(IContrato repo) : ControllerBase
{
  private readonly IContrato _repo = repo;

  [HttpGet("{id}")]
  public async Task<IActionResult> ObtenerContratoPorId(int id)
  {
    // Usamos el método que ya existe en tu RepoContrato.
    // Este método ya incluye la información del suscriptor, ¡perfecto!
    var contrato = await _repo.GetByIdAsync(id);

    if (contrato == null)
    {
      return NotFound($"No se encontró el contrato con el ID: {id}");
    }

    return Ok(contrato);
  }
  // -------------------------
}