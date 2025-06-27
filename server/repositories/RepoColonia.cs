using megaapi.data;
using megaapi.interfaces;
using megaapi.models;

namespace megaapi.repositories;

/// <summary>Repositorio de colonias.</summary>
/// <param name="dbContext">Inyección de dependencia DbContext.</param>
public class RepoColonia(MEGADbContext dbContext) : IColonia
{
  private readonly MEGADbContext _dbContext = dbContext;

  public Task<Colonia> CreateAsync(Colonia entity)
  {
    throw new NotImplementedException();
  }

  public Task<IEnumerable<Colonia>> GetAllAsync()
  {
    throw new NotImplementedException();
  }

  public Task<Colonia?> GetByIdAsync(int id)
  {
    throw new NotImplementedException();
  }

  public Task<bool> RemoveAsync(Colonia entity)
  {
    throw new NotImplementedException();
  }

  public Task<bool> UpdateAsync(Colonia entity)
  {
    throw new NotImplementedException();
  }
}