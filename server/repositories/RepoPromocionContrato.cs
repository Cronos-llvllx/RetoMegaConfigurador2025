using megaapi.data;
using megaapi.data.objects;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;

namespace megaapi.repositories;

public class RepoPromocionContrato(MEGADbContext dbContext) : IPromocionContrato
{
  private readonly MEGADbContext _dbContext = dbContext;
  public async Task<PromocionContrato> CrearAsync(PromocionContrato record)
  {
    await _dbContext.PromocionContrato.AddAsync(record);
    await _dbContext.SaveChangesAsync();

    return record;
  }

  public async Task<IEnumerable<PromocionContrato>> ObtenerTodoAsync()
  {
    return await _dbContext.PromocionContrato
      .Include(pC => pC.Promocion).ToListAsync();
  }

  public async Task<PromocionContrato?> ObtenerPorIdAsync(int[] id)
  {
    if (id.Length != 2)
      throw new ArgumentException("La cadena recibida debe contener solo dos elementos");
    else if (id[0] <= 0)
      throw new InvalidDataException($"La llave {id[0]} es inválida");
    else if (id[1] <= 0)
      throw new InvalidDataException($"La llave {id[1]} es inválida");

    return await _dbContext.PromocionContrato
      .Include(pC => pC.Promocion)
      .SingleOrDefaultAsync(pC => pC.Idpromocion == id[0] && pC.Idcontrato == id[1]);
  }

  public async Task<bool> EliminarAsync(PromocionContrato record)
  {
    _dbContext.PromocionContrato.Remove(record);

    var coincidencias = await _dbContext.SaveChangesAsync();

    return coincidencias > 0;
  }

  public Task<bool> ActualizarAsync(PromocionContrato record)
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<PromocionContrato>> ObtenerPorReferencia(int id, string nombreIdentificador)
  {
    return (await _dbContext.PromocionContrato
      .Include(pC => pC.Promocion)
      .ToListAsync())
      .Where(pC => OperadorObj<PromocionContrato, int>.Comparar(pC, nombreIdentificador, id));
  }
}