using megaapi.data;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace megaapi.repositories;

public class RepoCiudad(MEGADbContext dbContext) : ICiudad
{
    private readonly MEGADbContext _dbContext = dbContext;

    public static Ciudad ReducirCiudad(Ciudad ciudad)
    {
        return new Ciudad
        {
            Idciudad = ciudad.Idciudad,
            Nombre = ciudad.Nombre,
            Colonias = ciudad.Colonias.Select(col => new Colonia { Idcolonia = col.Idcolonia, Nombre = col.Nombre }).ToList()
        };
    }

    public Task<Ciudad> CrearAsync(Ciudad entity) => throw new NotImplementedException();
    public Task<bool> ActualizarAsync(Ciudad entity) => throw new NotImplementedException();
    public Task<bool> EliminarAsync(Ciudad entity) => throw new NotImplementedException();

    public async Task<IEnumerable<Ciudad>> ObtenerTodoAsync()
    {
        // CORREGIDO: Se usa 'Ciudades' en plural
        var auxCiudades = await _dbContext.Ciudades.Include(ciu => ciu.Colonias).ToListAsync();
        return auxCiudades.Select(ReducirCiudad);
    }

    public async Task<Ciudad?> ObtenerPorIdAsync(int id)
    {
        // CORREGIDO: Se usa 'Ciudades' en plural
        var auxCiudad = await _dbContext.Ciudades.Include(ciu => ciu.Colonias).SingleOrDefaultAsync(ciu => ciu.Idciudad == id);
        if (auxCiudad != null)
            auxCiudad = ReducirCiudad(auxCiudad);
        return auxCiudad;
    }
}