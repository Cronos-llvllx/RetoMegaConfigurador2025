using megaapi.data;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;

namespace megaapi.repositories;

public class RepoPaqueteServicio(MEGADbContext dbContext) : IPaqueteServicio
{
  private readonly MEGADbContext _dbContext = dbContext;
  public async Task<PaqueteServicio> CreateAsync(PaqueteServicio record)
  {
    await _dbContext.PaqueteServicio.AddAsync(record);
    await _dbContext.SaveChangesAsync();

    return record;
  }

  public async Task<IEnumerable<PaqueteServicio>> GetAllAsync()
  {
    return await _dbContext.PaqueteServicio.ToListAsync();
  }

  public async Task<PaqueteServicio?> GetByIdAsync(int[] id)
  {
    if (id.Length != 2)
      throw new ArgumentException("La cadena recibida debe contener solo dos elementos");
    else if (id[0] <= 0)
      throw new InvalidDataException($"La llave {id[0]} es inválida");
    else if (id[1] <= 0)
      throw new InvalidDataException($"La llave {id[1]} es inválida");

    return await _dbContext.PaqueteServicio.FindAsync(new { Idpromocion = id[0], Idciudad = id[1] });
  }

  public async Task<bool> RemoveAsync(PaqueteServicio record)
  {
    _dbContext.PaqueteServicio.Remove(record);

    var coincidencias = await _dbContext.SaveChangesAsync();

    return coincidencias > 0;
  }

  public Task<bool> UpdateAsync(PaqueteServicio record)
  {
    throw new NotImplementedException();
  }

  /// <summary>
  /// Obtiene los servicios relacionados a un paquete.
  /// </summary>
  /// <param name="paquete">El paquete del que se desea buscar.</param>
  public async Task<IEnumerable<PaqueteServicio>> ObtenerServicios(Paquete paquete)
  {
    return (await _dbContext.PaqueteServicio.ToListAsync())
      .Where(pS => pS.Idpaquete == paquete.Idpaquete);
  }
}