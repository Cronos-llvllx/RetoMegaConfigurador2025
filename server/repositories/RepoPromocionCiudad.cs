using megaapi.data;
using megaapi.data.objects;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;

namespace megaapi.repositories;

public class RepoPromocionCiudad(MEGADbContext dbContext) : IPromocionCiudad
{
  private readonly MEGADbContext _dbContext = dbContext;
  public async Task<PromocionCiudad> CrearAsync(PromocionCiudad record)
  {
    await _dbContext.PromocionCiudad.AddAsync(record);
    await _dbContext.SaveChangesAsync();

    return record;
  }

  public async Task<IEnumerable<PromocionCiudad>> ObtenerTodoAsync()
  {
    return await _dbContext.PromocionCiudad
      .Include(pC => pC.Promocion)
      .ToListAsync();
  }

  public async Task<PromocionCiudad?> ObtenerPorIdAsync(int[] id)
  {
    if (id.Length != 2)
      throw new ArgumentException("La cadena recibida debe contener solo dos elementos");
    else if (id[0] <= 0)
      throw new InvalidDataException($"La llave {id[0]} es inválida");
    else if (id[1] <= 0)
      throw new InvalidDataException($"La llave {id[1]} es inválida");

    return await _dbContext.PromocionCiudad
      .Include(pC => pC.Promocion)
      .SingleOrDefaultAsync(pC => pC.Idpromocion == id[0] && pC.Idciudad == id[1] );
  }

  public async Task<bool> EliminarAsync(PromocionCiudad record)
  {
    _dbContext.PromocionCiudad.Remove(record);

    var coincidencias = await _dbContext.SaveChangesAsync();

    return coincidencias > 0;
  }

  public Task<bool> ActualizarAsync(PromocionCiudad record)
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<PromocionCiudad>> ObtenerPorReferencia(int id, string nombreIdentificador)
  {
    return (await _dbContext.PromocionCiudad
      .Include(pC => pC.Promocion)
      .ToListAsync())
      .Where(pC => OperadorObj<PromocionCiudad, int>.Comparar(pC, nombreIdentificador, id));
  }
}