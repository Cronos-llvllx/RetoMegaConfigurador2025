using System.ComponentModel.DataAnnotations;
using megaapi.data.objects;
using megaapi.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace megaapi.controllers;

/// <summary>
/// Controlador para calcular deudas.
/// </summary>
/// <param name="dbContext">Dependencia al DbContext.</param>
[ApiController]
[Route("api/[controller]")]
public class Deuda(IContrato repoContrato, IContratoPaquete repoContratoPaquete, IPaqueteServicio repoPaqueteServicio, IPromocionCiudad repoPromoCiudad, IPromocionColonia repoPromoColonia, IPromocionPaquete repoPromoPaquete) : ControllerBase
{
  private readonly IContrato _repoContrato = repoContrato;
  private readonly IContratoPaquete _repoContratoPaquete = repoContratoPaquete;
  private readonly IPaqueteServicio _repoPaqueteServicio = repoPaqueteServicio;
  private readonly IPromocionCiudad _repoPromoCiudad = repoPromoCiudad;
  private readonly IPromocionColonia _repoPromoColonia = repoPromoColonia;
  private readonly IPromocionPaquete _repoPromoPaquete = repoPromoPaquete;

  [HttpPost("calcular")]
  public async Task<IActionResult> CalcularDeuda([FromBody] CalculoDto calculo)
  {
    try
    {
      // Verifica el modelo recibido.
      if (!ModelState.IsValid)
        throw new InvalidDataException("Modelo recibido inválido.");

      // Verifica las fechas recibidas.
      if (!DateTime.TryParse(calculo.FechaInicio, out DateTime fechaInicio))
        throw new InvalidDataException("Formato de FechaInicio inválido.");

      if (!DateTime.TryParse(calculo.FechaFin, out DateTime fechaFin))
        throw new InvalidDataException("Formato de FechaFin inválido.");

      // *Necesitas obtener el contrato solicitado (información del suscriptor incluida).
      var contratoRepo = await _repoContrato.ObtenerPorIdAsync(calculo.Idcontrato)
          ?? throw new NullReferenceException($"No existe un contrato con id {calculo.Idcontrato}.");

      // Asigna el contrato a un objeto de extención.
      var contrato = new ContratoExt
      {
        Idcontrato = contratoRepo.Idcontrato,
        Idsuscriptor = contratoRepo.Idsuscriptor,
        FechaContr = contratoRepo.FechaContr,
        FechaFin = contratoRepo.FechaFin,
        Paquetes = [],
        PrecioBase = contratoRepo.PrecioBase,
        Suscriptor = contratoRepo.Suscriptor
      };

      // *Necesitas obtener los paquetes y servicios ligados al contrato.
      var contratoPaquetes = (await _repoContratoPaquete
        .ObtenerPorReferencia(contratoRepo.Idcontrato, "Idcontrato"))
        .ToList();

      foreach (var conPaq in contratoPaquetes)
      {
        // Para cada paquete, obtener los servicios ligados y agregar el paquete al contrato.
        var servicios = await _repoPaqueteServicio
            .ObtenerPorReferencia(conPaq.Idpaquete, "Idpaquete");

        // Agrega el paquete al contrato.
        contrato.Paquetes.Add(new PaqueteContratoExt
        {
          Idpaquete = conPaq.Idpaquete,
          Nombre = conPaq.Paquete.Nombre,
          FechaAdicion = conPaq.FechaAdicion,
          FechaRetiro = conPaq.FechaRetiro,
          PrecioBase = conPaq.Paquete.PrecioBase,
          Tipo = conPaq.Paquete.Tipo,
          Servicios = [.. servicios.Select(s => s.Servicio)]
        });
      }

      // *Necesitas obtener todas las promociones:
      //  - Por ciudads.
      var ciudadPromos = (await _repoPromoCiudad
        .ObtenerTodoAsync())
        .ToList();

      //  - Por colonias.
      var coloniaPromos = (await _repoPromoColonia
        .ObtenerTodoAsync())
        .ToList();

      //  - Por paquete incluido en el contrato del suscriptor.
      List<models.PromocionPaquete> paquetesPromos = [];

      foreach (var conPaq in contrato.Paquetes)
      {
        // Busca por cada paquete en el contrato.
        var paqPromos = (await _repoPromoPaquete
          .ObtenerPorReferencia(conPaq.Idpaquete, "Idpaquete"))
          .ToList();

        // Agrega la promoción de paquete al arreglo de promociones de paquetes.
        paqPromos.ForEach(paquetesPromos.Add);
      }

      // *Realiza el cálculo de la deuda.
      return Ok(data.objects.Deuda.Calcular(
        fechaInicio,
        fechaFin,
        contrato,
        ciudadPromos,
        coloniaPromos,
        paquetesPromos
      ));
    }
    catch (InvalidDataException ex)
    {
      return BadRequest(ex.Message);
    }
    catch (NullReferenceException ex)
    {
      return BadRequest(ex);
    }
    catch (Exception ex)
    {
      return StatusCode(500, ex);
    }
  }
}

public class CalculoDto
{
  [Required]
  public int Idcontrato { get; set; }
  [Required]
  public string FechaInicio { get; set; } = null!;
  [Required]
  public string FechaFin { get; set; } = null!;
}