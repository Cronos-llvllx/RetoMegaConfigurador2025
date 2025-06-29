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
    return auxList.Select(s => new Suscriptor
    {
      Idsuscriptor = s.Idsuscriptor,
      Idcolonia = s.Idcolonia,
      Email = s.Email,
      Nombre = s.Nombre,
      Telefono = s.Telefono,
      Tipo = s.Tipo,
      Colonia = new Colonia
      {
        IdColonia = s.Idcolonia,
        Idciudad = s.Colonia.Idciudad,
        Nombre = s.Colonia.Nombre,
        Ciudad = new Ciudad
        {
          Idciudad = s.Colonia.Idciudad,
          Nombre = s.Colonia.Ciudad.Nombre
        }
      }
    });
  }

  public async Task<Suscriptor?> ObtenerPorIdAsync(int id)
  {
    var auxSus = (await _dbContext.Suscriptor
      .Include(s => s.Colonia)
      .Include(s => s.Colonia.Ciudad)
      .ToListAsync()
      ).Where(s => s.Idsuscriptor == id);

    return auxSus.Select(s => new Suscriptor
    {
      Idsuscriptor = s.Idsuscriptor,
      Idcolonia = s.Idcolonia,
      Email = s.Email,
      Nombre = s.Nombre,
      Telefono = s.Telefono,
      Tipo = s.Tipo,
      Colonia = new Colonia
      {
        IdColonia = s.Idcolonia,
        Idciudad = s.Colonia.Idciudad,
        Nombre = s.Colonia.Nombre,
        Ciudad = new Ciudad
        {
          Idciudad = s.Colonia.Idciudad,
          Nombre = s.Colonia.Ciudad.Nombre
        }
      }
    }).FirstOrDefault();
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