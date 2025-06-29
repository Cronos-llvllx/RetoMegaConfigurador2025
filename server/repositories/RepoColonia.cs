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

  public Task<Colonia> CreateAsync(Colonia colonia)
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<Colonia>> GetAllAsync()
  {
    return await _dbContext.Colonias.ToListAsync();
  }

  public async Task<Colonia?> GetByIdAsync(int id)
  {
    return await _dbContext.Colonias.FindAsync(id);
  }

  public Task<bool> RemoveAsync(Colonia colonia)
  {
    throw new NotImplementedException();
  }

  public Task<bool> UpdateAsync(Colonia colonia)
  {
    throw new NotImplementedException();
  }
}