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

  public Task<Ciudad> CrearAsync(Ciudad entity)
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<Ciudad>> ObtenerTodoAsync()
  {
    return await _dbContext.Ciudad.ToListAsync();
  }

  public async Task<Ciudad?> ObtenerPorIdAsync(int id)
  {
    return await _dbContext.Ciudad.FindAsync(id);
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
