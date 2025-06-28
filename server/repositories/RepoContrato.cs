using megaapi.data;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;

namespace megaapi.repositories;

/// <summary>Repositorio de contratos.</summary>
/// <param name="dbContext">Inyección de dependencia DbContext.</param>
public class RepoContrato(MEGADbContext dbContext) : IContrato
{
  private readonly MEGADbContext _dbContext = dbContext;

  public Task<Contrato> CreateAsync(Contrato contrato)
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<Contrato>> GetAllAsync()
  {
    return await _dbContext.Contrato.ToListAsync();
  }

  public async Task<Contrato?> GetByIdAsync(int id)
  {
    return await _dbContext.Contrato.FindAsync(id);
  }

  public Task<bool> RemoveAsync(Contrato contrato)
  {
    throw new NotImplementedException();
  }

  public Task<bool> UpdateAsync(Contrato contrato)
  {
    throw new NotImplementedException();
  }
}