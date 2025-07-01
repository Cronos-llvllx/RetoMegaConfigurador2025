using megaapi.data;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace megaapi.repositories;

public class RepoPaquete(MEGADbContext dbContext) : IPaquete
{
  private readonly MEGADbContext _dbContext = dbContext;

  public async Task<Paquete> ActualizarAsync(Paquete paquete)
  {
    _dbContext.Paquetes.Update(paquete);
    await _dbContext.SaveChangesAsync();

    return paquete;
  }

  public async Task<bool> CrearAsync(Paquete paquete)
  {
    _dbContext.Paquetes.Add(paquete);
    await _dbContext.SaveChangesAsync();

    return true;
  }

  public async Task<bool> EliminarAsync(Paquete paquete)
  {
    _dbContext.Paquetes.Remove(paquete);
    await _dbContext.SaveChangesAsync();

    return true;
  }

  public async Task<Paquete?> ObtenerPorIdAsync(int id)
  {
    return await _dbContext.Paquetes
      .Include(p => p.Servicios)
      .SingleOrDefaultAsync(p => p.Idpaquete == id);
  }

  public async Task<IEnumerable<Paquete>> ObtenerTodoAsync()
  {
    // CORREGIDO: Se usa 'Paquetes' en plural
    return await _dbContext.Paquetes
      .Include(p => p.Servicios)
      .ToListAsync();
  }
}