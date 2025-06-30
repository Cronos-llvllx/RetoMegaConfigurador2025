using megaapi.data;
using megaapi.data.objects;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace megaapi.repositories;

public class RepoPaqueteServicio(MEGADbContext dbContext) : IPaqueteServicio
{
    private readonly MEGADbContext _dbContext = dbContext;
    public async Task<PaqueteServicio> CrearAsync(PaqueteServicio record)
    {
        // CORREGIDO: Se usa 'PaqueteServicios' en plural
        await _dbContext.PaqueteServicios.AddAsync(record);
        await _dbContext.SaveChangesAsync();
        return record;
    }
    public async Task<IEnumerable<PaqueteServicio>> ObtenerTodoAsync()
    {
        // CORREGIDO: Se usa 'PaqueteServicios' en plural
        return await _dbContext.PaqueteServicios.Include(pS => pS.Servicio).ToListAsync();
    }
    public async Task<PaqueteServicio?> ObtenerPorIdAsync(int[] id)
    {
        if (id.Length != 2) throw new ArgumentException("La cadena recibida debe contener solo dos elementos");
        // CORREGIDO: Se usa 'PaqueteServicios' en plural
        return await _dbContext.PaqueteServicios.Include(pS => pS.Servicio).SingleOrDefaultAsync(pS => pS.Idpaquete == id[0] && pS.Idservicio == id[1]);
    }
    public async Task<bool> EliminarAsync(PaqueteServicio record)
    {
        // CORREGIDO: Se usa 'PaqueteServicios' en plural
        _dbContext.PaqueteServicios.Remove(record);
        var coincidencias = await _dbContext.SaveChangesAsync();
        return coincidencias > 0;
    }
    public async Task<IEnumerable<PaqueteServicio>> ObtenerPorReferencia(int id, string nombreIdentificador)
    {
        // CORREGIDO: Se usa 'PaqueteServicios' en plural
        return (await _dbContext.PaqueteServicios.Include(pS => pS.Servicio).ToListAsync()).Where(pC => OperadorObj<PaqueteServicio, int>.Comparar(pC, nombreIdentificador, id));
    }
    public Task<bool> ActualizarAsync(PaqueteServicio record) => throw new NotImplementedException();
}