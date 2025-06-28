using megaapi.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace megaapi.controllers;

/// <summary>Controlador de la API para contratos.</summary>
/// <param name="repo">Inyección de dependencia del repositorio.</param>
[ApiController]
[Route("api/[controller]")]
public class CContrato(IContrato repo) : ControllerBase
{
  private readonly IContrato _repo = repo;

  // ** Definir endpoints.
}