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

  public Task<Suscriptor> CreateAsync(Suscriptor suscriptor)
  {
    throw new NotImplementedException();
  }

  public async Task<IEnumerable<Suscriptor>> GetAllAsync()
  {
    // Incluye información de la colonia y de la ciudad.
    var suscriptores = await _dbContext.Suscriptor
      .Include(s => s.Colonia)
      .Include(s => s.Colonia.Ciudad)
      .ToListAsync();

    // Omite los arreglos de colonias en Ciudad (Colonias) y suscriptores en Colonia (Suscriptores)
    // para evitar bucles.
    var resultado = suscriptores.Select(s => new Suscriptor
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
        Nombre = s.Colonia.Nombre,
        Ciudad = new Ciudad
        {
          Idciudad = s.Colonia.Ciudad.Idciudad,
          Nombre = s.Colonia.Ciudad.Nombre
        }
      }
    }).ToList();

    return resultado;
  }



  public async Task<Suscriptor?> GetByIdAsync(int id)
  {
    return await _dbContext.Suscriptor.FindAsync(id);
  }

  public Task<bool> RemoveAsync(Suscriptor suscriptor)
  {
    throw new NotImplementedException();
  }

  public Task<bool> UpdateAsync(Suscriptor suscriptor)
  {
    throw new NotImplementedException();
  }
}