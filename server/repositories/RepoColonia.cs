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

  public Task<Colonia> CrearAsync(Colonia colonia)
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<Colonia>> ObtenerTodoAsync()
  {
    return await _dbContext.Colonia.ToListAsync();
  }

  public async Task<Colonia?> ObtenerPorIdAsync(int id)
  {
    return await _dbContext.Colonia.FindAsync(id);
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