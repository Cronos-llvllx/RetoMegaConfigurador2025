using megaapi.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace megaapi.controllers;


/// <summary>Controlador de la API para paquetes.</summary>
/// <param name="repo">Inyección de dependencia del repositorio.</param>
[ApiController]
[Route("api/[controller]")]
public class Paquete(IPaquete repo, IContratoPaquete repoCotratoPaquete, IPaqueteServicio repoPaqueteServicio) : ControllerBase
{
  private readonly IPaquete _repo = repo;
  private readonly IContratoPaquete _repoCotratoPaquete = repoCotratoPaquete;
  private readonly IPaqueteServicio _repoPaqueteServicio = repoPaqueteServicio;

  // ** Definir endpoints...
  [HttpGet("")]
  public async Task<IActionResult> ObtenerTodosPaquetes()
  {
    try
    {
      var paquetes = await _repo.ObtenerTodoAsync();
      // Devolver solo los datos básicos para evitar problemas de serialización
      var paquetesSimplificados = paquetes.Select(p => new {
        idpaquete = p.Idpaquete,
        nombre = p.Nombre,
        precioBase = p.PrecioBase,
        tipo = p.Tipo
      });
      return Ok(paquetesSimplificados);
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }

  [HttpGet("admin/completos")]
  public async Task<IActionResult> ObtenerPaquetesCompletos()
  {
    try
    {
      var paquetes = await _repo.ObtenerTodoAsync();
      // Devolver paquetes con servicios para gestión completa
      var paquetesCompletos = paquetes.Select(p => new {
        idpaquete = p.Idpaquete,
        nombre = p.Nombre,
        precioBase = p.PrecioBase,
        tipo = p.Tipo,
        servicios = p.Servicios?.Select(ps => new {
          idservicio = ps.Idservicio,
          idpaquete = ps.Idpaquete
        }).ToArray() ?? new object[0]
      });
      return Ok(paquetesCompletos);
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> ObtenerPaquetePorId(int id)
  {
    try
    {
      var dbPaquete = await _repo.ObtenerPorIdAsync(id)
        ?? throw new NullReferenceException($"No se encontró un paquete con el id {id}");

      return Ok(dbPaquete);
    }
    catch (NullReferenceException ex)
    {
      return NotFound(ex.Message);
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }

  [HttpPost("registrar")]
  public async Task<IActionResult> RegistrarPaquete([FromBody] PaqueteDto paquete)
  {
    try
    {
      if (!ModelState.IsValid)
        throw new InvalidDataException("Modelo recibido inválido.");

      // Registra un nuevo paquete.
      var dbPaquete = new models.Paquete
      {
        Nombre = paquete.Nombre,
        PrecioBase = paquete.PrecioBase,
        Tipo = paquete.Tipo
      };

      dbPaquete = await _repo.CrearAsync(dbPaquete);

      // Registra los servicios.
      foreach (var serv in paquete.Servicios)
      {
        var dbServ = new models.PaqueteServicio
        {
          Idpaquete = dbPaquete.Idpaquete,
          Idservicio = serv
        };

        // Registra el servicio.
        await _repoPaqueteServicio.CrearAsync(dbServ);
      }

      return Ok(dbPaquete);
    }
    catch (InvalidDataException ex)
    {
      return BadRequest(ex.Message);
    }
    catch (NullReferenceException ex)
    {
      return BadRequest(ex.Message);
    }
    catch (Exception ex)
    {
      Console.Error.WriteLine(ex);
      return StatusCode(500, ex.Message);
    }
  }

  [HttpDelete("eliminar/{id}")]
  public async Task<IActionResult> EliminarPaquete(int id)
  {
    try
    {
      var dbPaquete = await _repo.ObtenerPorIdAsync(id)
        ?? throw new NullReferenceException($"No se encontró un paquete con el id {id}");

      // Revisa relaciones del paquete con contratos.
      var dbContratos = await _repoCotratoPaquete.ObtenerPorReferencia(dbPaquete.Idpaquete, "Idpaquete");

      if (dbContratos.Any())
        throw new UnauthorizedAccessException("No se puede eliminar el paquete porque ya está relacionado con contratos");

      // Elimina las relaciones con los servicios.
      var dbServicios = await _repoPaqueteServicio.ObtenerPorReferencia(dbPaquete.Idpaquete, "Idpaquete");

      foreach (var serv in dbServicios)
        await _repoPaqueteServicio.EliminarAsync(serv);

      return Ok(await _repo.EliminarAsync(dbPaquete));
    }
    catch (NullReferenceException ex)
    {
      return NotFound(ex.Message);
    }
    catch (UnauthorizedAccessException ex)
    {
      return Unauthorized(ex.Message);
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }

  [HttpPut("actualizar/{idPaquete}")]
  public async Task<IActionResult> ActualizarPaquete(int idPaquete, PaqueteDto paquete)
  {
    try
    {
      if (!ModelState.IsValid)
        throw new InvalidDataException("Modelo recibido inválido.");

      var dbPaquete = await _repo.ObtenerPorIdAsync(idPaquete)
        ?? throw new NullReferenceException($"No se encontró el paquete con id {idPaquete}");

      dbPaquete.Nombre = paquete.Nombre;
      dbPaquete.PrecioBase = paquete.PrecioBase;

      if (dbPaquete.Tipo != paquete.Tipo)
      {
        // Revisa relaciones del paquete con contratos.
        var dbContratos = await _repoCotratoPaquete.ObtenerTodoAsync();

        if (dbContratos.ToList().Find(c => c.Idpaquete == dbPaquete.Idpaquete) != null)
          throw new UnauthorizedAccessException("No se puede actualizar el tipo de este paquete porque ya está relacionado con contratos");
      }

      // Actualiza el tipo después de las validaciones
      dbPaquete.Tipo = paquete.Tipo;

      // Actualiza el paquete.
      dbPaquete = await _repo.ActualizarAsync(dbPaquete);

      // Revisa los servicios.
      if (paquete.Servicios.Count > 0)
      {
              // Elimna los servicios actualmente registrados.
        var dbServicios = await _repoPaqueteServicio.ObtenerPorReferencia(dbPaquete.Idpaquete, "Idpaquete");

        foreach (var serv in dbServicios)
          await _repoPaqueteServicio.EliminarAsync(serv);

        // Registra los nuevos servicios.
        foreach (var serv in paquete.Servicios)
        {
          var paqueteServicio = new models.PaqueteServicio
          {
            Idpaquete = dbPaquete.Idpaquete,
            Idservicio = serv
          };

          await _repoPaqueteServicio.CrearAsync(paqueteServicio);
        }
      }

      return Ok(dbPaquete);
    }
    catch (InvalidDataException ex)
    {
      return BadRequest(ex.Message);
    }
    catch (NullReferenceException ex)
    {
      return BadRequest(ex.Message);
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex.Message);
    }
  }
}

public class PaqueteDto
{
  public int Idpaquete { get; set; }
  public string Nombre { get; set; } = null!;
  public decimal PrecioBase { get; set; }
  public byte Tipo { get; set; }
  public ICollection<int> Servicios { get; set; } = [];
}