using megaapi.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace megaapi.controllers;

/// <summary>Controlador de la API para promociones.</summary>
/// <param name="repoPromocion">Inyección de dependencia del repositorio.</param>
[ApiController]
[Route("api/[controller]")]
public class Promocion(IPromocion repoPromocion, IPromocionCiudad repoPromocionCiudad, IPromocionColonia repoPromocionColonia, IPromocionPaquete repoPromocionPaquete) : ControllerBase
{
  private readonly IPromocion _repo = repoPromocion;
  private readonly IPromocionCiudad _repoPromocionCiudad = repoPromocionCiudad;
  private readonly IPromocionColonia _repoPromocionColonia = repoPromocionColonia;
  private readonly IPromocionPaquete _repoPromocionPaquete = repoPromocionPaquete;

  // ** Definir endpoints...
  [HttpGet("")]
  public async Task<IActionResult> ObtenerPromociones()
  {
    return Ok(await _repo.ObtenerTodoAsync());
  }

  [HttpGet("{id}")]
  public async Task<IActionResult> ObtenerPromocionPorId(int id)
  {
    try
    {
      var promocion = await _repo.ObtenerPorIdAsync(id)
        ?? throw new NullReferenceException();

      return Ok(promocion);
    }
    catch (NullReferenceException)
    {
      return NotFound($"No se encontró la promoción con el id {id}");
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex);
    }
  }

  [HttpPost("registro")]
  public async Task<IActionResult> RegistrarPromocion([FromBody] PromocionRegistroDto promo)
  {
    try
    {
      if (!ModelState.IsValid)
        throw new InvalidDataException("Modelo recibido inválido.");

      if (!DateTime.TryParse(promo.Vigencia, out DateTime vigenciaFecha))
        throw new InvalidDataException("Formato de fecha vigencia inválida.");

      if (promo.Tipo == 1
        && (promo.Ciudaddes.Count > 0
        || promo.Colonias.Count > 0
        || promo.Paquetes.Count > 0
      ))
        throw new InvalidDataException("Las promociones de contratación no pueden tener relaciones con ciudades, colonias ni paquetes.");
      else if (promo.Tipo == 2 && promo.Ciudaddes.Count > 0 && promo.Colonias.Count > 0)
        throw new InvalidDataException("No pueden haber colonias y ciudades a la vez");
      else if (promo.Tipo == 2 && promo.Ciudaddes.Count == 0 && promo.Colonias.Count < 0 && promo.Paquetes.Count < 0)
        throw new InvalidDataException("La promocion debe aplicar para uno al menos: Ciudades, Colonias, Paquetes");

      // Crea una promoción.
      var promoModel = new models.Promocion
      {
        Alcance = promo.Tipo == 1 ? null : promo.Alcance,
        Ciudades = [],
        Colonias = [],
        Contratos = [],
        Duracion = promo.Duracion,
        Nombre = promo.Nombre,
        PrecioPorcen = promo.PrecioPorcen,
        Tipo = promo.Tipo,
        Vigencia = vigenciaFecha
      };

      // Registra la promoción.
      promoModel = await _repo.CrearAsync(promoModel);

      // Registra promociones por paquete solo si es promoción para mensualidades.
      if (promoModel.Tipo == 2)
      {
        // Registro por colonia.
        foreach (var col in promo.Colonias)
        {
          var promoCol = new models.PromocionColonia
          {
            Idcolonia = col,
            Idpromocion = promoModel.Idpromocion
          };

          await _repoPromocionColonia.CrearAsync(promoCol);
        }

        // Registro por ciudad (solo si no hay colonias).
        if (promo.Colonias.Count == 0)
        {
          foreach (var ciu in promo.Ciudaddes)
          {

            var promoCol = new models.PromocionCiudad
            {
              Idciudad = ciu,
              Idpromocion = promoModel.Idpromocion
            };

            await _repoPromocionCiudad.CrearAsync(promoCol);
          }
        }

        // Registro por paquete.
        foreach (var paq in promo.Paquetes)
        {
          var promoCol = new models.PromocionPaquete
          {
            Idpaquete = paq,
            Idpromocion = promoModel.Idpromocion
          };

          await _repoPromocionPaquete.CrearAsync(promoCol);
        }
      }

      return Ok(promoModel);
    }
    catch (InvalidDataException ex)
    {
      return BadRequest(ex.Message);
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex);
    }
  }
}

public class PromocionRegistroDto
{
  public byte? Alcance { get; set; }
  public string Nombre { get; set; } = null!;
  public int Duracion { get; set; }
  public decimal PrecioPorcen { get; set; }
  public byte Tipo { get; set; }
  public string Vigencia { get; set; } = null!;
  public ICollection<int> Ciudaddes { get; set; } = [];
  public ICollection<int> Colonias { get; set; } = [];
  public ICollection<int> Paquetes { get; set; } = [];
}