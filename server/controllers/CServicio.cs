using megaapi.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace megaapi.controllers;


/// <summary>Controlador de la API para servicios.</summary>
/// <param name="repo">Inyección de dependencia del repositorio.</param>
[ApiController]
[Route("api/[controller]")]
public class Servicio(IServicio repo) : ControllerBase
{
  private readonly IServicio _repo = repo;

  // ** Definir endpoints...
  [HttpGet]
  [Route("")]
  public async Task<IActionResult> ObtenerServicios()
  {
    return Ok(await _repo.GetAllAsync());
  }
}