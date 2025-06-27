using megaapi.data;
using megaapi.interfaces;
using megaapi.models;

namespace megaapi.repositories;

/// <summary>Repositorio de promociones personalizadas.</summary>
/// <param name="dbContext">Inyección de dependencia DbContext.</param>
public class RepoPromopersonalizada(MEGADbContext dbContext) : IPromoPersonalizada
{
  private readonly MEGADbContext _dbContext = dbContext;

  public Task<PromoPersonalizada> CreateAsync(PromoPersonalizada entity)
  {
    throw new NotImplementedException();
  }

  public Task<IEnumerable<PromoPersonalizada>> GetAllAsync()
  {
    throw new NotImplementedException();
  }

  public Task<PromoPersonalizada?> GetByIdAsync(int id)
  {
    throw new NotImplementedException();
  }

  public Task<bool> RemoveAsync(PromoPersonalizada entity)
  {
    throw new NotImplementedException();
  }

  public Task<bool> UpdateAsync(PromoPersonalizada entity)
  {
    throw new NotImplementedException();
  }
}