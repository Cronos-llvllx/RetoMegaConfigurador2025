using megaapi.data;
using megaapi.interfaces;
using megaapi.models;

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

  public Task<IEnumerable<Ciudad>> GetAllAsync()
  {
    throw new NotImplementedException();
  }

  public Task<Ciudad?> GetByIdAsync(int id)
  {
    throw new NotImplementedException();
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
