using megaapi.data;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;

namespace megaapi.repositories
{
  /// <summary>Repositorio de promociones.</summary>
  /// <param name="dbContext">Inyección de dependencia DbContext.</param>
  public class RepoPromocion(MEGADbContext dbContext) : IPromocion
  {
    private readonly MEGADbContext _dbContext = dbContext;

    public Task<Promocion> CrearAsync(Promocion promocion)
    {
      throw new NotImplementedException();
    }

    public async Task<IEnumerable<Promocion>> ObtenerTodoAsync()
    {
      return await _dbContext.Promocion.ToListAsync();
    }

    public async Task<Promocion?> ObtenerPorIdAsync(int id)
    {
      return await _dbContext.Promocion.FindAsync(id);
    }

    public Task<bool> EliminarAsync(Promocion promocion)
    {
      throw new NotImplementedException();
    }

    public Task<bool> ActualizarAsync(Promocion promocion)
    {
      throw new NotImplementedException();
    }
  }
}