using megaapi.data;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace megaapi.repositories;

public class RepoPromocion(MEGADbContext dbContext) : IPromocion
{
  private readonly MEGADbContext _dbContext = dbContext;
  public async Task<IEnumerable<Promocion>> ObtenerTodoAsync()
  {
    // CORREGIDO: Se usa 'Promociones' en plural
    return await _dbContext.Promociones
      .Include(p => p.Ciudades)
      .Include(p => p.Colonias)
      .Include(p => p.Paquetes)
      .ToListAsync();
  }
  public async Task<Promocion?> ObtenerPorIdAsync(int id)
  {
    // CORREGIDO: Se usa 'Promociones' en plural
    return await _dbContext.Promociones
      .Include(p => p.Ciudades)
      .Include(p => p.Colonias)
      .Include(p => p.Paquetes)
      .SingleOrDefaultAsync(p => p.Idpromocion == id);

  }
  public async Task<Promocion> CrearAsync(Promocion promocion)
  {
    await _dbContext.Promociones.AddAsync(promocion);
    await _dbContext.SaveChangesAsync();

    return promocion;
  }

  public async Task<bool> ActualizarAsync(Promocion promocion)
  {
    _dbContext.Promociones.Update(promocion);
    await _dbContext.SaveChangesAsync();
    return true;
  }
  public Task<bool> EliminarAsync(Promocion promocion) => throw new NotImplementedException();
}