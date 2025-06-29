using megaapi.data;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;

namespace megaapi.repositories;

/// <summary>Repositorio de ciudades.</summary>
/// <param name="dbContext">Inyección de dependencia DbContext.</param>
public class RepoCiudad(MEGADbContext dbContext) : ICiudad
{
  private readonly MEGADbContext _dbContext = dbContext;

  /// <summary>
  /// Reduce la información de una ciudad para evitar bucles infinitos.
  /// </summary>
  /// <param name="ciudad">La instancia a reducir.</param>
  public static Ciudad ReducirCiudad(Ciudad ciudad)
  {
    return new Ciudad
    {
      Idciudad = ciudad.Idciudad,
      Nombre = ciudad.Nombre,
      Colonias = [.. ciudad.Colonias.Select(col => new Colonia
      {
        Idcolonia = col.Idcolonia,
        Nombre = col.Nombre
      })]
    };
  }

  public Task<Ciudad> CrearAsync(Ciudad entity)
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<Ciudad>> ObtenerTodoAsync()
  {
    var auxCiudades = await _dbContext.Ciudad
      .Include(ciu => ciu.Colonias)
      .ToListAsync();

    return auxCiudades.Select(ReducirCiudad);
  }

  public async Task<Ciudad?> ObtenerPorIdAsync(int id)
  {
    var auxCiudad = await _dbContext.Ciudad
      .Include(ciu => ciu.Colonias)
      .SingleAsync(ciu => ciu.Idciudad == id);

    if (auxCiudad != null)
      auxCiudad = ReducirCiudad(auxCiudad);

    return auxCiudad;
  }

  public Task<bool> EliminarAsync(Ciudad entity)
  {
    throw new NotImplementedException();
  }

  public Task<bool> ActualizarAsync(Ciudad entity)
  {
    throw new NotImplementedException();
  }
}
