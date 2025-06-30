using megaapi.data;
using megaapi.data.objects;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace megaapi.repositories;

public class RepoContratoPaquete(MEGADbContext dbContext) : IContratoPaquete
{
    private readonly MEGADbContext _dbContext = dbContext;
    public async Task<ContratoPaquete> CrearAsync(ContratoPaquete record)
    {
        // CORREGIDO: Se usa 'ContratoPaquetes' en plural
        await _dbContext.ContratoPaquetes.AddAsync(record);
        await _dbContext.SaveChangesAsync();
        return record;
    }
    public async Task<IEnumerable<ContratoPaquete>> ObtenerTodoAsync()
    {
        // CORREGIDO: Se usa 'ContratoPaquetes' en plural
        return await _dbContext.ContratoPaquetes.Include(cP => cP.Paquete).ToListAsync();
    }
    public async Task<ContratoPaquete?> ObtenerPorIdAsync(int[] id)
    {
        if (id.Length != 2) throw new ArgumentException("La cadena recibida debe contener solo dos elementos");
        // CORREGIDO: Se usa 'ContratoPaquetes' en plural
        return await _dbContext.ContratoPaquetes.Include(cP => cP.Paquete).SingleOrDefaultAsync(cP => cP.Idcontrato == id[0] && cP.Idpaquete == id[1]);
    }
    public async Task<bool> EliminarAsync(ContratoPaquete record)
    {
        // CORREGIDO: Se usa 'ContratoPaquetes' en plural
        _dbContext.ContratoPaquetes.Remove(record);
        var coincidencias = await _dbContext.SaveChangesAsync();
        return coincidencias > 0;
    }
    public async Task<IEnumerable<ContratoPaquete>> ObtenerPorReferencia(int id, string nombreIdentificador)
    {
        // CORREGIDO: Se usa 'ContratoPaquetes' en plural
        return (await _dbContext.ContratoPaquetes.Include(cP => cP.Paquete).ToListAsync()).Where(pC => OperadorObj<ContratoPaquete, int>.Comparar(pC, nombreIdentificador, id));
    }
    public Task<bool> ActualizarAsync(ContratoPaquete record) => throw new NotImplementedException();
}