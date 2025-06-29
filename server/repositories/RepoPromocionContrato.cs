using megaapi.data;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;

namespace megaapi.repositories;

public class RepoPromocionContrato(MEGADbContext dbContext) : IPromocionContrato
{
  private readonly MEGADbContext _dbContext = dbContext;
  public async Task<PromocionContrato> CreateAsync(PromocionContrato record)
  {
    await _dbContext.PromocionContrato.AddAsync(record);
    await _dbContext.SaveChangesAsync();

    return record;
  }

  public async Task<IEnumerable<PromocionContrato>> GetAllAsync()
  {
    return await _dbContext.PromocionContrato.ToListAsync();
  }

  public async Task<PromocionContrato?> GetByIdAsync(int[] id)
  {
    if (id.Length != 2)
      throw new ArgumentException("La cadena recibida debe contener solo dos elementos");
    else if (id[0] <= 0)
      throw new InvalidDataException($"La llave {id[0]} es inválida");
    else if (id[1] <= 0)
      throw new InvalidDataException($"La llave {id[1]} es inválida");

    return await _dbContext.PromocionContrato.FindAsync(new { Idpromocion = id[0], Idciudad = id[1] });
  }

  public async Task<bool> RemoveAsync(PromocionContrato record)
  {
    _dbContext.PromocionContrato.Remove(record);

    var coincidencias = await _dbContext.SaveChangesAsync();

    return coincidencias > 0;
  }

  public Task<bool> UpdateAsync(PromocionContrato record)
  {
    throw new NotImplementedException();
  }
}