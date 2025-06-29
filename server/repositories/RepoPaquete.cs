using megaapi.data;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;

namespace megaapi.repositories;

/// <summary>Repositorio de paquetes.</summary>
/// <param name="dbContext">Inyección de dependencia DbContext.</param>
public class RepoPaquete(MEGADbContext dbContext) : IPaquete
{
  private readonly MEGADbContext _dbContext = dbContext;

  public Task<Paquete> CrearAsync(Paquete paquete)
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<Paquete>> ObtenerTodoAsync()
  {
    return await _dbContext.Paquetes.ToListAsync();
  }

  public async Task<Paquete?> ObtenerPorIdAsync(int id)
  {
    return await _dbContext.Paquetes.FindAsync(id);
  }

  public Task<bool> EliminarAsync(Paquete paquete)
  {
    throw new NotImplementedException();
  }

  public Task<bool> ActualizarAsync(Paquete paquete)
  {
    throw new NotImplementedException();
  }
}