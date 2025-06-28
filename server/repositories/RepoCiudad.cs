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

  public Task<Ciudad> CreateAsync(Ciudad entity)
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<Ciudad>> GetAllAsync()
  {
    return await _dbContext.Ciudad.ToListAsync();
  }

  public async Task<Ciudad?> GetByIdAsync(int id)
  {
    return await _dbContext.Ciudad.FindAsync(id);
  }

  public Task<bool> RemoveAsync(Ciudad entity)
  {
    throw new NotImplementedException();
  }

  public Task<bool> UpdateAsync(Ciudad entity)
  {
    throw new NotImplementedException();
  }
}
