using megaapi.data;
using megaapi.data.objects;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;

namespace megaapi.repositories;

public class RepoPaqueteServicio(MEGADbContext dbContext) : IPaqueteServicio
{
  private readonly MEGADbContext _dbContext = dbContext;
  public async Task<PaqueteServicio> CrearAsync(PaqueteServicio record)
  {
    await _dbContext.PaqueteServicio.AddAsync(record);
    await _dbContext.SaveChangesAsync();

    return record;
  }

  public async Task<IEnumerable<PaqueteServicio>> ObtenerTodoAsync()
  {
    return await _dbContext.PaqueteServicio
      .Include(pS => pS.Servicio)
      .ToListAsync();
  }

  public async Task<PaqueteServicio?> ObtenerPorIdAsync(int[] id)
  {
    if (id.Length != 2)
      throw new ArgumentException("La cadena recibida debe contener solo dos elementos");
    else if (id[0] <= 0)
      throw new InvalidDataException($"La llave {id[0]} es inválida");
    else if (id[1] <= 0)
      throw new InvalidDataException($"La llave {id[1]} es inválida");

    return await _dbContext.PaqueteServicio
      .Include(pS => pS.Servicio)
      .SingleOrDefaultAsync(pS => pS.Idpaquete == id[0] && pS.Idservicio == id[1]);
  }

  public async Task<bool> EliminarAsync(PaqueteServicio record)
  {
    _dbContext.PaqueteServicio.Remove(record);

    var coincidencias = await _dbContext.SaveChangesAsync();

    return coincidencias > 0;
  }

  public Task<bool> ActualizarAsync(PaqueteServicio record)
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<PaqueteServicio>> ObtenerPorReferencia(int id, string nombreIdentificador)
  {
    return (await _dbContext.PaqueteServicio
      .Include(pS => pS.Servicio)
      .ToListAsync())
      .Where(pC => OperadorObj<PaqueteServicio, int>.Comparar(pC, nombreIdentificador, id));
  }
}