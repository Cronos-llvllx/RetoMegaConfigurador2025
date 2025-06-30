using megaapi.data;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace megaapi.repositories;
public class RepoServicio(MEGADbContext dbContext) : IServicio
{
  private readonly MEGADbContext _dbContext = dbContext;

  public Task<Servicio> CrearAsync(Servicio servicio) => throw new NotImplementedException();
  public Task<bool> ActualizarAsync(Servicio servicio) => throw new NotImplementedException();
  public Task<bool> EliminarAsync(Servicio servicio) => throw new NotImplementedException();

  public async Task<IEnumerable<Servicio>> ObtenerTodoAsync()
  {
    
    return await _dbContext.Servicios.ToListAsync();
  }

  public async Task<Servicio?> ObtenerPorIdAsync(int id)
  {
    // CORREGIDO: Se usa 'Servicios' en plural
    return await _dbContext.Servicios.FindAsync(id);
  }
}