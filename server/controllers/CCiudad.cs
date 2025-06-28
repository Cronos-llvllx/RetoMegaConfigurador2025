using megaapi.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace megaapi.controllers;

/// <summary>Controlador de la API para ciudades.</summary>
/// <param name="repo">Inyección de dependencia del repositorio.</param>
[ApiController]
[Route("api/[controller]")]
public class CCiudad(ICiudad repo) : ControllerBase
{
  /// <summary>Repositorio de ciudades.</summary>
  private readonly ICiudad _repo = repo;

  // ** Definir endpoints...
}