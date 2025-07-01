using megaapi.data;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;

namespace megaapi.repositories;

public class RepoPromoPersonalizada(MEGADbContext dbContext) : IPromoPersonalizada
{
  private readonly MEGADbContext _dbContext = dbContext;

  public Task<PromoPersonalizada> CrearAsync(PromoPersonalizada promo) => throw new NotImplementedException();
  public Task<bool> ActualizarAsync(PromoPersonalizada promo) => throw new NotImplementedException();
  public Task<bool> EliminarAsync(PromoPersonalizada promo) => throw new NotImplementedException();

  public async Task<IEnumerable<PromoPersonalizada>> ObtenerTodoAsync()
  {
    // CORREGIDO: Se usa 'PromoPersonalizadas' en plural
    return await _dbContext.PromoPersonalizadas.ToListAsync();
  }

  public async Task<PromoPersonalizada?> ObtenerPorIdAsync(int id)
  {
    // CORREGIDO: Se usa 'PromoPersonalizadas' en plural
    return await _dbContext.PromoPersonalizadas.FindAsync(id);
  }
}