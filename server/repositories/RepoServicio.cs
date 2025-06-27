using megaapi.data;
using megaapi.interfaces;
using megaapi.models;

namespace megaapi.repositories;

/// <summary>Repositorio de servicios.</summary>
/// <param name="dbContext">Inyección de dependencia DbContext.</param>
public class RepoServicio(MEGADbContext dbContext) : IServicio
{
  private readonly MEGADbContext _dbContext = dbContext;

  public Task<Servicio> CreateAsync(Servicio entity)
  {
    throw new NotImplementedException();
  }

  public Task<IEnumerable<Servicio>> GetAllAsync()
  {
    throw new NotImplementedException();
  }

  public Task<Servicio?> GetByIdAsync(int id)
  {
    throw new NotImplementedException();
  }

  public Task<bool> RemoveAsync(Servicio entity)
  {
    throw new NotImplementedException();
  }

  public Task<bool> UpdateAsync(Servicio entity)
  {
    throw new NotImplementedException();
  }
}