using megaapi.data;
using megaapi.interfaces;
using megaapi.models;

namespace megaapi.repositories;

/// <summary>Repositorio de suscriptores.</summary>
/// <param name="dbContext">Inyección de dependencia DbContext.</param>
public class RepoSuscriptor(MEGADbContext dbContext) : ISuscriptor
{
  private readonly MEGADbContext _dbContext = dbContext;

  public Task<Suscriptor> CreateAsync(Suscriptor entity)
  {
    throw new NotImplementedException();
  }

  public Task<IEnumerable<Suscriptor>> GetAllAsync()
  {
    throw new NotImplementedException();
  }

  public Task<Suscriptor?> GetByIdAsync(int id)
  {
    throw new NotImplementedException();
  }

  public Task<bool> RemoveAsync(Suscriptor entity)
  {
    throw new NotImplementedException();
  }

  public Task<bool> UpdateAsync(Suscriptor entity)
  {
    throw new NotImplementedException();
  }
}