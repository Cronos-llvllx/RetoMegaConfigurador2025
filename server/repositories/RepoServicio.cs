using megaapi.data;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;

namespace megaapi.repositories;

/// <summary>Repositorio de servicios.</summary>
/// <param name="dbContext">Inyección de dependencia DbContext.</param>
public class RepoServicio(MEGADbContext dbContext) : IServicio
{
  private readonly MEGADbContext _dbContext = dbContext;

  public Task<Servicio> CrearAsync(Servicio servicio)
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<Servicio>> ObtenerTodoAsync()
  {
    return await _dbContext.Servicio.ToListAsync();
  }

  public async Task<Servicio?> ObtenerPorIdAsync(int id)
  {
    return await _dbContext.Servicio.FindAsync(id);
  }

  public Task<bool> EliminarAsync(Servicio servicio)
  {
    throw new NotImplementedException();
  }

  public Task<bool> ActualizarAsync(Servicio servicio)
  {
    throw new NotImplementedException();
  }
}