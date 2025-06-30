using megaapi.data;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;

namespace megaapi.repositories;

/// <summary>Repositorio de promociones personalizadas.</summary>
/// <param name="dbContext">Inyección de dependencia DbContext.</param>
public class RepoPromoPersonalizada(MEGADbContext dbContext) : IPromoPersonalizada
{
  private readonly MEGADbContext _dbContext = dbContext;

  public Task<PromoPersonalizada> CrearAsync(PromoPersonalizada promo)
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<PromoPersonalizada>> ObtenerTodoAsync()
  {
    return await _dbContext.PromoPersonalizada.ToListAsync();
  }

  public async Task<PromoPersonalizada?> ObtenerPorIdAsync(int id)
  {
    return await _dbContext.PromoPersonalizada.FindAsync(id);
  }

  public Task<bool> EliminarAsync(PromoPersonalizada promo)
  {
    throw new NotImplementedException();
  }

  public Task<bool> ActualizarAsync(PromoPersonalizada promo)
  {
    throw new NotImplementedException();
  }
}