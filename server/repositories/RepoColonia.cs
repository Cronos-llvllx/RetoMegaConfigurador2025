using megaapi.data;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;

namespace megaapi.repositories;

/// <summary>Repositorio de colonias.</summary>
/// <param name="dbContext">Inyección de dependencia DbContext.</param>
public class RepoColonia(MEGADbContext dbContext) : IColonia
{
  private readonly MEGADbContext _dbContext = dbContext;

  /// <summary>
  /// Reduce la información de una colonia para evitar bucles infinitos.
  /// </summary>
  /// <param name="colonia">La instancia a reducir.</param>
  public static Colonia ReducirColonia(Colonia colonia)
  {
    return new Colonia
    {
      Idcolonia = colonia.Idcolonia,
      Idciudad = colonia.Idciudad,
      Nombre = colonia.Nombre,
      Ciudad = new Ciudad
      {
        Idciudad = colonia.Ciudad.Idciudad,
        Nombre = colonia.Ciudad.Nombre
      }
    };
  }

  public Task<Colonia> CrearAsync(Colonia colonia)
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<Colonia>> ObtenerTodoAsync()
  {
    var auxColonias = await _dbContext.Colonia
      .Include(col => col.Ciudad)
      .ToListAsync();

    return auxColonias.Select(ReducirColonia);
  }

  public async Task<Colonia?> ObtenerPorIdAsync(int id)
  {
    var auxColonia = await _dbContext.Colonia
      .Include(col => col.Ciudad)
      .SingleAsync(col => col.Idcolonia == id);

    if (auxColonia != null)
      auxColonia = ReducirColonia(auxColonia);

    return auxColonia;
  }

  public Task<bool> EliminarAsync(Colonia colonia)
  {
    throw new NotImplementedException();
  }

  public Task<bool> ActualizarAsync(Colonia colonia)
  {
    throw new NotImplementedException();
  }
}