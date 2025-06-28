using megaapi.data;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;

namespace megaapi.repositories;

/// <summary>Repositorio de suscriptores.</summary>
/// <param name="dbContext">Inyección de dependencia DbContext.</param>
public class RepoSuscriptor(MEGADbContext dbContext) : ISuscriptor
{
  private readonly MEGADbContext _dbContext = dbContext;

  public Task<Suscriptor> CreateAsync(Suscriptor suscriptor)
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<Suscriptor>> GetAllAsync()
  {
    return await _dbContext.Suscriptores.ToListAsync();
  }

  public async Task<Suscriptor?> GetByIdAsync(int id)
  {
    return await _dbContext.Suscriptores.FindAsync(id);
  }

  public Task<bool> RemoveAsync(Suscriptor suscriptor)
  {
    throw new NotImplementedException();
  }

  public Task<bool> UpdateAsync(Suscriptor suscriptor)
  {
    throw new NotImplementedException();
  }
}