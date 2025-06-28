using megaapi.data;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;

namespace megaapi.repositories;

/// <summary>Repositorio de servicios.</summary>
/// <param name="dbContext">Inyección de dependencia DbContext.</param>
public class RepoServicio(MEGADbContext dbContext) : IServicio
{
  private readonly MEGADbContext _dbContext = dbContext;

  public Task<Servicio> CreateAsync(Servicio servicio)
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<Servicio>> GetAllAsync()
  {
    return await _dbContext.Servicios.ToListAsync();
  }

  public async Task<Servicio?> GetByIdAsync(int id)
  {
    return await _dbContext.Servicios.FindAsync(id);
  }

  public Task<bool> RemoveAsync(Servicio servicio)
  {
    throw new NotImplementedException();
  }

  public Task<bool> UpdateAsync(Servicio servicio)
  {
    throw new NotImplementedException();
  }
}