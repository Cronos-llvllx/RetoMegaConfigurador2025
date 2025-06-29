using megaapi.data;
using megaapi.data.objects;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;

namespace megaapi.repositories;

public class RepoContratoPaquete(MEGADbContext dbContext) : IContratoPaquete
{
  private readonly MEGADbContext _dbContext = dbContext;
  public async Task<ContratoPaquete> CrearAsync(ContratoPaquete record)
  {
    await _dbContext.ContratoPaquete.AddAsync(record);
    await _dbContext.SaveChangesAsync();

    return record;
  }

  public async Task<IEnumerable<ContratoPaquete>> ObtenerTodoAsync()
  {
    return await _dbContext.ContratoPaquete
      .Include(cP => cP.Paquete)
      .ToListAsync();
  }

  public async Task<ContratoPaquete?> ObtenerPorIdAsync(int[] id)
  {
    if (id.Length != 2)
      throw new ArgumentException("La cadena recibida debe contener solo dos elementos");
    else if (id[0] <= 0)
      throw new InvalidDataException($"La llave {id[0]} es inválida");
    else if (id[1] <= 0)
      throw new InvalidDataException($"La llave {id[1]} es inválida");

    return await _dbContext.ContratoPaquete
      .Include(cP => cP.Paquete)
      .SingleOrDefaultAsync(cP => cP.Idcontrato == id[0] && cP.Idpaquete == id[1]);
  }

  public async Task<bool> EliminarAsync(ContratoPaquete record)
  {
    _dbContext.ContratoPaquete.Remove(record);

    var coincidencias = await _dbContext.SaveChangesAsync();

    return coincidencias > 0;
  }

  public Task<bool> ActualizarAsync(ContratoPaquete record)
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<ContratoPaquete>> ObtenerPorReferencia(int id, string nombreIdentificador)
  {
    return (await _dbContext.ContratoPaquete
      .Include(cP => cP.Paquete)
      .ToListAsync())
      .Where(pC => OperadorObj<ContratoPaquete, int>.Comparar(pC, nombreIdentificador, id));
  }
}