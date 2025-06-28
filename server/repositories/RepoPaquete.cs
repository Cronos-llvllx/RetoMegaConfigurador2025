using megaapi.data;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;

namespace megaapi.repositories;

/// <summary>Repositorio de paquetes.</summary>
/// <param name="dbContext">Inyección de dependencia DbContext.</param>
public class RepoPaquete(MEGADbContext dbContext) : IPaquete
{
  private readonly MEGADbContext _dbContext = dbContext;

  public Task<Paquete> CreateAsync(Paquete paquete)
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<Paquete>> GetAllAsync()
  {
    return await _dbContext.Paquetes.ToListAsync();
  }

  public async Task<Paquete?> GetByIdAsync(int id)
  {
    return await _dbContext.Paquetes.FindAsync(id);
  }

  public Task<bool> RemoveAsync(Paquete paquete)
  {
    throw new NotImplementedException();
  }

  public Task<bool> UpdateAsync(Paquete paquete)
  {
    throw new NotImplementedException();
  }
}