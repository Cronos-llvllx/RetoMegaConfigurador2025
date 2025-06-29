using megaapi.data;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;

namespace megaapi.repositories;

public class RepoPromocionPaquete(MEGADbContext dbContext) : IEntidad<PromocionPaquete, int[]>
{
  private readonly MEGADbContext _dbContext = dbContext;
  public async Task<PromocionPaquete> CreateAsync(PromocionPaquete record)
  {
    await _dbContext.PromocionPaquete.AddAsync(record);
    await _dbContext.SaveChangesAsync();

    return record;
  }

  public async Task<IEnumerable<PromocionPaquete>> GetAllAsync()
  {
    return await _dbContext.PromocionPaquete.ToListAsync();
  }

  public async Task<PromocionPaquete?> GetByIdAsync(int[] id)
  {
    if (id.Length != 2)
      throw new ArgumentException("La cadena recibida debe contener solo dos elementos");
    else if (id[0] <= 0)
      throw new InvalidDataException($"La llave {id[0]} es inválida");
    else if (id[1] <= 0)
      throw new InvalidDataException($"La llave {id[1]} es inválida");

    return await _dbContext.PromocionPaquete.FindAsync(new { Idpromocion = id[0], Idciudad = id[1] });
  }

  public async Task<bool> RemoveAsync(PromocionPaquete record)
  {
    _dbContext.PromocionPaquete.Remove(record);

    var coincidencias = await _dbContext.SaveChangesAsync();

    return coincidencias > 0;
  }

  public Task<bool> UpdateAsync(PromocionPaquete record)
  {
    throw new NotImplementedException();
  }
}