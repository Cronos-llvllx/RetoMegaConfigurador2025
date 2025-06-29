using megaapi.data;
using megaapi.data.objects;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;

namespace megaapi.repositories;

public class RepoPromocionColonia(MEGADbContext dbContext) : IPromocionColonia
{
  private readonly MEGADbContext _dbContext = dbContext;
  public async Task<PromocionColonia> CrearAsync(PromocionColonia record)
  {
    await _dbContext.PromocionColonia.AddAsync(record);
    await _dbContext.SaveChangesAsync();

    return record;
  }

  public async Task<IEnumerable<PromocionColonia>> ObtenerTodoAsync()
  {
    return await _dbContext.PromocionColonia.ToListAsync();
  }

  public async Task<PromocionColonia?> ObtenerPorIdAsync(int[] id)
  {
    if (id.Length != 2)
      throw new ArgumentException("La cadena recibida debe contener solo dos elementos");
    else if (id[0] <= 0)
      throw new InvalidDataException($"La llave {id[0]} es inválida");
    else if (id[1] <= 0)
      throw new InvalidDataException($"La llave {id[1]} es inválida");

    return await _dbContext.PromocionColonia.FindAsync(new { Idpromocion = id[0], Idciudad = id[1] });
  }

  public async Task<bool> EliminarAsync(PromocionColonia record)
  {
    _dbContext.PromocionColonia.Remove(record);

    var coincidencias = await _dbContext.SaveChangesAsync();

    return coincidencias > 0;
  }

  public Task<bool> ActualizarAsync(PromocionColonia record)
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<PromocionColonia>> ObtenerPorReferencia(int id)
  {
    return (await _dbContext.PromocionColonia.ToListAsync())
      .Where(pC => pC.Idpromocion == id);
  }

  public async Task<IEnumerable<PromocionColonia>> ObtenerPorReferencia(int id, string nombreIdentificador)
  {
    return (await _dbContext.PromocionColonia.ToListAsync())
      .Where(pC => OperadorObj<PromocionColonia, int>.Comparar(pC, nombreIdentificador, id));
  }
}