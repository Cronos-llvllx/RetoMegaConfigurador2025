using megaapi.data;
using megaapi.data.objects;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;

namespace megaapi.repositories;

public class RepoPromocionPaquete(MEGADbContext dbContext) : IPromocionPaquete
{
  private readonly MEGADbContext _dbContext = dbContext;
  public async Task<PromocionPaquete> CrearAsync(PromocionPaquete record)
  {
    await _dbContext.PromocionPaquete.AddAsync(record);
    await _dbContext.SaveChangesAsync();

    return record;
  }

  public async Task<IEnumerable<PromocionPaquete>> ObtenerTodoAsync()
  {
    return await _dbContext.PromocionPaquete.ToListAsync();
  }

  public async Task<PromocionPaquete?> ObtenerPorIdAsync(int[] id)
  {
    if (id.Length != 2)
      throw new ArgumentException("La cadena recibida debe contener solo dos elementos");
    else if (id[0] <= 0)
      throw new InvalidDataException($"La llave {id[0]} es inválida");
    else if (id[1] <= 0)
      throw new InvalidDataException($"La llave {id[1]} es inválida");

    return await _dbContext.PromocionPaquete.FindAsync(new { Idpromocion = id[0], Idciudad = id[1] });
  }

  public async Task<bool> EliminarAsync(PromocionPaquete record)
  {
    _dbContext.PromocionPaquete.Remove(record);

    var coincidencias = await _dbContext.SaveChangesAsync();

    return coincidencias > 0;
  }

  public Task<bool> ActualizarAsync(PromocionPaquete record)
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<PromocionPaquete>> ObtenerPorReferencia(int id, string nombreIdentificador)
  {
    return (await _dbContext.PromocionPaquete
       .Include(pP => pP.Promocion)
       .ToListAsync())
      .Where(pC => OperadorObj<PromocionPaquete, int>.Comparar(pC, nombreIdentificador, id));
  }
}