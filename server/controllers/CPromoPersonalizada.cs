using megaapi.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace megaapi.controllers;

/// <summary>Controlador de la API para promociones personalizadas.</summary>
/// <param name="repo">Inyección de dependencia del repositorio.</param>
[ApiController]
[Route("api/[controller]")]
public class PromoPersonalizada(IPromocion repo) : ControllerBase
{
  private readonly IPromocion _repo = repo;

  // ** Definir endpoints...
}