using megaapi.data;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;

namespace megaapi.repositories;

public class RepoPromocionColonia(MEGADbContext dbContext) : IPromocionColonia
{
  private readonly MEGADbContext _dbContext = dbContext;
  public async Task<PromocionColonia> CreateAsync(PromocionColonia record)
  {
    await _dbContext.PromocionColonia.AddAsync(record);
    await _dbContext.SaveChangesAsync();

    return record;
  }

  public async Task<IEnumerable<PromocionColonia>> GetAllAsync()
  {
    return await _dbContext.PromocionColonia.ToListAsync();
  }

  public async Task<PromocionColonia?> GetByIdAsync(int[] id)
  {
    if (id.Length != 2)
      throw new ArgumentException("La cadena recibida debe contener solo dos elementos");
    else if (id[0] <= 0)
      throw new InvalidDataException($"La llave {id[0]} es inválida");
    else if (id[1] <= 0)
      throw new InvalidDataException($"La llave {id[1]} es inválida");

    return await _dbContext.PromocionColonia.FindAsync(new { Idpromocion = id[0], Idciudad = id[1] });
  }

  public async Task<bool> RemoveAsync(PromocionColonia record)
  {
    _dbContext.PromocionColonia.Remove(record);

    var coincidencias = await _dbContext.SaveChangesAsync();

    return coincidencias > 0;
  }

  public Task<bool> UpdateAsync(PromocionColonia record)
  {
    throw new NotImplementedException();
  }

  /// <summary>
  /// Obtiene las colonias relacionadas a una promoción.
  /// </summary>
  /// <param name="promocion">La promoción de la que se desea buscar.</param>
  public async Task<IEnumerable<PromocionColonia>> ObtenerCiudades(Promocion promocion)
  {
    return (await _dbContext.PromocionColonia.ToListAsync())
      .Where(pC => pC.Idpromocion == promocion.Idpromocion);
  }
}