using megaapi.data;
using megaapi.interfaces;
using megaapi.models;

namespace megaapi.repositories;

/// <summary>Repositorio de promociones.</summary>
/// <param name="dbContext">Inyección de dependencia DbContext.</param>
public class RepoPromocion(MEGADbContext dbContext) : IPromocion
{
  private readonly MEGADbContext _dbContext = dbContext;

  public Task<Promocion> CreateAsync(Promocion entity)
  {
    throw new NotImplementedException();
  }

  public Task<IEnumerable<Promocion>> GetAllAsync()
  {
    throw new NotImplementedException();
  }

  public Task<Promocion?> GetByIdAsync(int id)
  {
    throw new NotImplementedException();
  }

  public Task<bool> RemoveAsync(Promocion entity)
  {
    throw new NotImplementedException();
  }

  public Task<bool> UpdateAsync(Promocion entity)
  {
    throw new NotImplementedException();
  }
}