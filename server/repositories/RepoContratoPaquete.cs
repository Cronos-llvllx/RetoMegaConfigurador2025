using megaapi.data;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;

namespace megaapi.repositories;

public class RepoContratoPaquete(MEGADbContext dbContext) : IContratoPaquete
{
  private readonly MEGADbContext _dbContext = dbContext;
  public async Task<ContratoPaquete> CreateAsync(ContratoPaquete record)
  {
    await _dbContext.ContratoPaquete.AddAsync(record);
    await _dbContext.SaveChangesAsync();

    return record;
  }

  public async Task<IEnumerable<ContratoPaquete>> GetAllAsync()
  {
    return await _dbContext.ContratoPaquete.ToListAsync();
  }

  public async Task<ContratoPaquete?> GetByIdAsync(int[] id)
  {
    if (id.Length != 2)
      throw new ArgumentException("La cadena recibida debe contener solo dos elementos");
    else if (id[0] <= 0)
      throw new InvalidDataException($"La llave {id[0]} es inválida");
    else if (id[1] <= 0)
      throw new InvalidDataException($"La llave {id[1]} es inválida");

    return await _dbContext.ContratoPaquete.FindAsync(new { Idpromocion = id[0], Idciudad = id[1] });
  }

  public async Task<bool> RemoveAsync(ContratoPaquete record)
  {
    _dbContext.ContratoPaquete.Remove(record);

    var coincidencias = await _dbContext.SaveChangesAsync();

    return coincidencias > 0;
  }

  public Task<bool> UpdateAsync(ContratoPaquete record)
  {
    throw new NotImplementedException();
  }

  /// <summary>
  /// Obtiene los paquetes relacionados a un contrato.
  /// </summary>
  /// <param name="contrato">El contrato del que se desea buscar.</param>
  public async Task<IEnumerable<ContratoPaquete>> ObtenerPaquetes(Contrato contrato)
  {
    return (await _dbContext.ContratoPaquete.ToListAsync())
      .Where(cP => cP.Idcontrato == contrato.Idcontrato);
  }
}