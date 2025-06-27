using megaapi.data;
using megaapi.interfaces;
using megaapi.models;

namespace megaapi.repositories;

/// <summary>Repositorio de paquetes.</summary>
/// <param name="dbContext">Inyección de dependencia DbContext.</param>
public class RepoPaquete(MEGADbContext dbContext) : IPaquete
{
  private readonly MEGADbContext _dbContext = dbContext;

  public Task<Paquete> CreateAsync(Paquete entity)
  {
    throw new NotImplementedException();
  }

  public Task<IEnumerable<Paquete>> GetAllAsync()
  {
    throw new NotImplementedException();
  }

  public Task<Paquete?> GetByIdAsync(int id)
  {
    throw new NotImplementedException();
  }

  public Task<bool> RemoveAsync(Paquete entity)
  {
    throw new NotImplementedException();
  }

  public Task<bool> UpdateAsync(Paquete entity)
  {
    throw new NotImplementedException();
  }
}