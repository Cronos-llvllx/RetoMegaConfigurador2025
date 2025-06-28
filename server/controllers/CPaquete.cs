using megaapi.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace megaapi.controllers;


/// <summary>Controlador de la API para paquetes.</summary>
/// <param name="repo">Inyección de dependencia del repositorio.</param>
[ApiController]
[Route("api/[controller]")]
public class Paquete(IPaquete repo) : ControllerBase
{
  private readonly IPaquete _repo = repo;

  // ** Definir endpoints...
}