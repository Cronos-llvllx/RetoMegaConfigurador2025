using megaapi.data;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;

namespace megaapi.repositories;

/// <summary>Repositorio de contratos.</summary>
/// <param name="dbContext">Inyección de dependencia DbContext.</param>
public class RepoContrato(MEGADbContext dbContext) : IContrato
{
  private readonly MEGADbContext _dbContext = dbContext;

  /// <summary>
  /// Reduce la información de un contrato para evitar bucles infinitos.
  /// </summary>
  /// <param name="contrato">La instancia a reducir.</param>
  public static Contrato ReducirContrato(Contrato contrato)
  {
    return new Contrato
    {
      Idcontrato = contrato.Idcontrato,
      Idsuscriptor = contrato.Idsuscriptor,
      PrecioBase = contrato.PrecioBase,
      FechaContr = contrato.FechaContr,
      FechaFin = contrato.FechaFin,
      Suscriptor = new Suscriptor
      {
        Idsuscriptor = contrato.Suscriptor.Idsuscriptor,
        Idcolonia = contrato.Suscriptor.Idcolonia,
        Email = contrato.Suscriptor.Email,
        Nombre = contrato.Suscriptor.Nombre,
        Telefono = contrato.Suscriptor.Telefono,
        Tipo = contrato.Suscriptor.Tipo,
        Colonia = new Colonia
        {
          Idcolonia = contrato.Suscriptor.Colonia.Idcolonia,
          Idciudad = contrato.Suscriptor.Colonia.Idciudad,
          Nombre = contrato.Suscriptor.Colonia.Nombre,
          Ciudad = new Ciudad
          {
            Idciudad = contrato.Suscriptor.Colonia.Ciudad.Idciudad,
            Nombre = contrato.Suscriptor.Colonia.Ciudad.Nombre
          }
        }
      }
    };
  }

  public Task<Contrato> CrearAsync(Contrato contrato)
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<Contrato>> ObtenerTodoAsync()
  {
    // Incluye toda la información del suscriptor ligado.
    var auxContratos = await _dbContext.Contrato
      .Include(con => con.Suscriptor)
      .Include(con => con.Suscriptor.Colonia)
      .Include(con => con.Suscriptor.Colonia.Ciudad)
      .ToListAsync();

    // Refactoriza la información para evitar bucles infinitos.
    return auxContratos.Select(ReducirContrato);
  }

  public async Task<Contrato?> ObtenerPorIdAsync(int id)
  {
    // Incluye toda la información del suscriptor ligado.
    var auxContrato = await _dbContext.Contrato
      .Include(con => con.Suscriptor)
      .Include(con => con.Suscriptor.Colonia)
      .Include(con => con.Suscriptor.Colonia.Ciudad)
      .SingleAsync(con => con.Idcontrato == id);

    // Refactoriza la información para evitar bucles infinitos.
    if (auxContrato != null)
      auxContrato = ReducirContrato(auxContrato);

    return auxContrato;
  }

  public Task<bool> EliminarAsync(Contrato contrato)
  {
    throw new NotImplementedException();
  }

  public Task<bool> ActualizarAsync(Contrato contrato)
  {
    throw new NotImplementedException();
  }
}