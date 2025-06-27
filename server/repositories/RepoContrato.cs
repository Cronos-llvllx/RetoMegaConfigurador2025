using megaapi.data;
using megaapi.interfaces;
using megaapi.models;

namespace megaapi.repositories;

/// <summary>Repositorio de contratos.</summary>
/// <param name="dbContext">Inyección de dependencia DbContext.</param>
public class RepoContrato(MEGADbContext dbContext) : IContrato
{
  private readonly MEGADbContext _dbContext = dbContext;

  public Task<Contrato> CreateAsync(Contrato entity)
  {
    throw new NotImplementedException();
  }

  public Task<IEnumerable<Contrato>> GetAllAsync()
  {
    throw new NotImplementedException();
  }

  public Task<Contrato?> GetByIdAsync(int id)
  {
    throw new NotImplementedException();
  }

  public Task<bool> RemoveAsync(Contrato entity)
  {
    throw new NotImplementedException();
  }

  public Task<bool> UpdateAsync(Contrato entity)
  {
    throw new NotImplementedException();
  }
}