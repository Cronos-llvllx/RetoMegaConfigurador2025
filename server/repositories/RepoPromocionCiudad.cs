using megaapi.data;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;

namespace megaapi.repositories;

public class RepoPromocionCiudad(MEGADbContext dbContext) : IPromocionCiudad
{
  private readonly MEGADbContext _dbContext = dbContext;
  public async Task<PromocionCiudad> CreateAsync(PromocionCiudad record)
  {
    await _dbContext.PromocionCiudad.AddAsync(record);
    await _dbContext.SaveChangesAsync();

    return record;
  }

  public async Task<IEnumerable<PromocionCiudad>> GetAllAsync()
  {
    return await _dbContext.PromocionCiudad.ToListAsync();
  }

  public async Task<PromocionCiudad?> GetByIdAsync(int[] id)
  {
    if (id.Length != 2)
      throw new ArgumentException("La cadena recibida debe contener solo dos elementos");
    else if (id[0] <= 0)
      throw new InvalidDataException($"La llave {id[0]} es inválida");
    else if (id[1] <= 0)
      throw new InvalidDataException($"La llave {id[1]} es inválida");

    return await _dbContext.PromocionCiudad.FindAsync(new { Idpromocion = id[0], Idciudad = id[1] });
  }

  public async Task<bool> RemoveAsync(PromocionCiudad record)
  {
    _dbContext.PromocionCiudad.Remove(record);

    var coincidencias = await _dbContext.SaveChangesAsync();

    return coincidencias > 0;
  }

  public Task<bool> UpdateAsync(PromocionCiudad record)
  {
    throw new NotImplementedException();
  }

  /// <summary>
  /// Obtiene las ciudades relacionadas a una promoción.
  /// </summary>
  /// <param name="promocion">La promoción de la que se desea buscar.</param>
  public async Task<IEnumerable<PromocionCiudad>> ObtenerCiudades(Promocion promocion)
  {
    return (await _dbContext.PromocionCiudad.ToListAsync())
      .Where(pC => pC.Idpromocion == promocion.Idpromocion);
  }
}