using megaapi.data;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace megaapi.repositories;

public class RepoColonia(MEGADbContext dbContext) : IColonia
{
    private readonly MEGADbContext _dbContext = dbContext;

    public static Colonia ReducirColonia(Colonia colonia)
    {
        return new Colonia
        {
            Idcolonia = colonia.Idcolonia,
            Idciudad = colonia.Idciudad,
            Nombre = colonia.Nombre,
            Ciudad = new Ciudad { Idciudad = colonia.Ciudad.Idciudad, Nombre = colonia.Ciudad.Nombre }
        };
    }
    
    public Task<Colonia> CrearAsync(Colonia colonia) => throw new NotImplementedException();
    public Task<bool> ActualizarAsync(Colonia colonia) => throw new NotImplementedException();
    public Task<bool> EliminarAsync(Colonia colonia) => throw new NotImplementedException();

    public async Task<IEnumerable<Colonia>> ObtenerTodoAsync()
    {
        // CORREGIDO: Se usa 'Colonias' en plural
        var auxColonias = await _dbContext.Colonias.Include(col => col.Ciudad).ToListAsync();
        return auxColonias.Select(ReducirColonia);
    }

    public async Task<Colonia?> ObtenerPorIdAsync(int id)
    {
        // CORREGIDO: Se usa 'Colonias' en plural
        var auxColonia = await _dbContext.Colonias.Include(col => col.Ciudad).SingleOrDefaultAsync(col => col.Idcolonia == id);
        if (auxColonia != null)
            auxColonia = ReducirColonia(auxColonia);
        return auxColonia;
    }

    public async Task<IEnumerable<Colonia>> ObtenerPorCiudadAsync(int ciudadId)
    {
        var auxColonias = await _dbContext.Colonias
            .Include(col => col.Ciudad)
            .Where(col => col.Idciudad == ciudadId)
            .ToListAsync();
        return auxColonias.Select(ReducirColonia);
    }
}