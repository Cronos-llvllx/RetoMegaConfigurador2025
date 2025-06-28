using megaapi.data;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;

namespace megaapi.repositories;

/// <summary>Repositorio de promociones personalizadas.</summary>
/// <param name="dbContext">Inyección de dependencia DbContext.</param>
public class RepoPromopersonalizada(MEGADbContext dbContext) : IPromoPersonalizada
{
  private readonly MEGADbContext _dbContext = dbContext;

  public Task<PromoPersonalizada> CreateAsync(PromoPersonalizada promo)
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<PromoPersonalizada>> GetAllAsync()
  {
    return await _dbContext.PromoPersonalizada.ToListAsync();
  }

  public async Task<PromoPersonalizada?> GetByIdAsync(int id)
  {
    return await _dbContext.PromoPersonalizada.FindAsync(id);
  }

  public Task<bool> RemoveAsync(PromoPersonalizada promo)
  {
    throw new NotImplementedException();
  }

  public Task<bool> UpdateAsync(PromoPersonalizada promo)
  {
    throw new NotImplementedException();
  }
}