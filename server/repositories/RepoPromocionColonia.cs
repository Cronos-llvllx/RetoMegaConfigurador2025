using megaapi.data;
using megaapi.data.objects;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace megaapi.repositories;
public class RepoPromocionColonia(MEGADbContext dbContext) : IPromocionColonia
{
    private readonly MEGADbContext _dbContext = dbContext;
    public async Task<PromocionColonia> CrearAsync(PromocionColonia record)
    {
        // CORREGIDO: Se usa 'PromocionColonias' en plural
        await _dbContext.PromocionColonias.AddAsync(record);
        await _dbContext.SaveChangesAsync();
        return record;
    }
    public async Task<IEnumerable<PromocionColonia>> ObtenerTodoAsync()
    {
        // CORREGIDO: Se usa 'PromocionColonias' en plural
        return await _dbContext.PromocionColonias.Include(pC => pC.Promocion).ToListAsync();
    }
    public async Task<PromocionColonia?> ObtenerPorIdAsync(int[] id)
    {
        if (id.Length != 2) throw new ArgumentException("La cadena recibida debe contener solo dos elementos");
        // CORREGIDO: Se usa 'PromocionColonias' en plural
        return await _dbContext.PromocionColonias.Include(pC => pC.Promocion).SingleOrDefaultAsync(pC => pC.Idpromocion == id[0] && pC.Idcolonia == id[1]);
    }
    public async Task<bool> EliminarAsync(PromocionColonia record)
    {
        // CORREGIDO: Se usa 'PromocionColonias' en plural
        _dbContext.PromocionColonias.Remove(record);
        return await _dbContext.SaveChangesAsync() > 0;
    }
    public async Task<IEnumerable<PromocionColonia>> ObtenerPorReferencia(int id, string nombreIdentificador)
    {
        // CORREGIDO: Se usa 'PromocionColonias' en plural
        return (await _dbContext.PromocionColonias.Include(pC => pC.Promocion).ToListAsync()).Where(pC => OperadorObj<PromocionColonia, int>.Comparar(pC, nombreIdentificador, id));
    }
    public Task<bool> ActualizarAsync(PromocionColonia record) => throw new NotImplementedException();
}