using megaapi.data;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;

namespace megaapi.repositories;

/// <summary>Repositorio de suscriptores.</summary>
/// <param name="dbContext">Inyección de dependencia DbContext.</param>
public class RepoSuscriptor(MEGADbContext dbContext) : ISuscriptor
{
  private readonly MEGADbContext _dbContext = dbContext;

  /// <summary>
  /// Reduce la información de un suscriptor para evitar bucles infinitos.
  /// </summary>
  /// <param name="suscriptor">La instancia a reducir.</param>
  public static Suscriptor ReducirSuscriptor(Suscriptor suscriptor)
  {
    return new Suscriptor
    {
      Idsuscriptor = suscriptor.Idsuscriptor,
      Idcolonia = suscriptor.Idcolonia,
      Email = suscriptor.Email,
      Nombre = suscriptor.Nombre,
      Telefono = suscriptor.Telefono,
      Tipo = suscriptor.Tipo,
      Colonia = new Colonia
      {
        Idcolonia = suscriptor.Idcolonia,
        Idciudad = suscriptor.Colonia.Idciudad,
        Nombre = suscriptor.Colonia.Nombre,
        Ciudad = new Ciudad
        {
          Idciudad = suscriptor.Colonia.Idciudad,
          Nombre = suscriptor.Colonia.Ciudad.Nombre
        }
      }
    };
  }

  public Task<Suscriptor> CrearAsync(Suscriptor suscriptor)
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<Suscriptor>> ObtenerTodoAsync()
  {
    // Incluye la información de la colonia y de la ciudad.
    var auxList = await _dbContext.Suscriptor
          .Include(s => s.Colonia)
          .Include(s => s.Colonia.Ciudad)
          .ToListAsync();

    // Filtra la información para evitar bucles infinitos.
    return auxList.Select(ReducirSuscriptor);
  }

  public async Task<Suscriptor?> ObtenerPorIdAsync(int id)
  {
    var auxSuscriptor = await _dbContext.Suscriptor
      .Include(s => s.Colonia)
      .Include(s => s.Colonia.Ciudad)
      .SingleAsync(s => s.Idsuscriptor == id);

    if (auxSuscriptor != null)
      auxSuscriptor = ReducirSuscriptor(auxSuscriptor);

    return auxSuscriptor;
  }

  public Task<bool> EliminarAsync(Suscriptor suscriptor)
  {
    throw new NotImplementedException();
  }

  public Task<bool> ActualizarAsync(Suscriptor suscriptor)
  {
    throw new NotImplementedException();
  }
}