using megaapi.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace megaapi.controllers;


/// <summary>Controlador de la API para servicios.</summary>
/// <param name="repo">Inyección de dependencia del repositorio.</param>
[ApiController]
[Route("api/[controller]")]
public class CServicio(IServicio repo) : ControllerBase
{
  private readonly IServicio _repo = repo;

  // ** Definir endpoints...
}