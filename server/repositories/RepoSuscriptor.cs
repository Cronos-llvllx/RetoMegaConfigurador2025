using megaapi.data;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace megaapi.repositories;
public class RepoSuscriptor(MEGADbContext dbContext) : ISuscriptor
{
    private readonly MEGADbContext _dbContext = dbContext;
    
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
                Idcolonia = suscriptor.Colonia.Idcolonia,
                Idciudad = suscriptor.Colonia.Idciudad,
                Nombre = suscriptor.Colonia.Nombre,
                Ciudad = new Ciudad
                {
                    Idciudad = suscriptor.Colonia.Ciudad.Idciudad,
                    Nombre = suscriptor.Colonia.Ciudad.Nombre
                }
            }
        };
    }

    public Task<Suscriptor> CrearAsync(Suscriptor suscriptor) => throw new NotImplementedException();
    public Task<bool> ActualizarAsync(Suscriptor suscriptor) => throw new NotImplementedException();
    public Task<bool> EliminarAsync(Suscriptor suscriptor) => throw new NotImplementedException();

    public async Task<IEnumerable<Suscriptor>> ObtenerTodoAsync()
    {
        // CORREGIDO: Se usa 'Suscriptores' en plural
        var auxList = await _dbContext.Suscriptores.Include(s => s.Colonia).ThenInclude(c => c.Ciudad).ToListAsync();
        return auxList.Select(ReducirSuscriptor);
    }

    public async Task<Suscriptor?> ObtenerPorIdAsync(int id)
    {
        // CORREGIDO: Se usa 'Suscriptores' en plural
        var auxSuscriptor = await _dbContext.Suscriptores.Include(s => s.Colonia).ThenInclude(c => c.Ciudad).SingleOrDefaultAsync(s => s.Idsuscriptor == id);
        if (auxSuscriptor != null)
            auxSuscriptor = ReducirSuscriptor(auxSuscriptor);
        return auxSuscriptor;
    }
}