using megaapi.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace megaapi.controllers;

/// <summary>Controlador de la API para colonias.</summary>
/// <param name="repo">Inyección de dependencia del repositorio.</param>
[ApiController]
[Route("api/[controller]")]
public class CColonia(IColonia repo) : ControllerBase
{
  private readonly IColonia _repo = repo;

  // ** Definir endpoints...
}