using megaapi.data;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;

namespace megaapi.repositories;

/// <summary>Repositorio de promociones.</summary>
/// <param name="dbContext">Inyección de dependencia DbContext.</param>
public class RepoPromocion(MEGADbContext dbContext) : IPromocion
{
  private readonly MEGADbContext _dbContext = dbContext;

  public Task<Promocion> CreateAsync(Promocion promocion)
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<Promocion>> GetAllAsync()
  {
    return await _dbContext.Promocion.ToListAsync();
  }

  public async Task<Promocion?> GetByIdAsync(int id)
  {
    return await _dbContext.Promocion.FindAsync(id);
  }

  public Task<bool> RemoveAsync(Promocion promocion)
  {
    throw new NotImplementedException();
  }

  public Task<bool> UpdateAsync(Promocion promocion)
  {
    throw new NotImplementedException();
  }
}