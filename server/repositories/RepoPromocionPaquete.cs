using megaapi.data;
using megaapi.data.objects;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace megaapi.repositories;
public class RepoPromocionPaquete(MEGADbContext dbContext) : IPromocionPaquete
{
    private readonly MEGADbContext _dbContext = dbContext;
    public async Task<PromocionPaquete> CrearAsync(PromocionPaquete record)
    {
        // CORREGIDO: Se usa 'PromocionPaquetes' en plural
        await _dbContext.PromocionPaquetes.AddAsync(record);
        await _dbContext.SaveChangesAsync();
        return record;
    }
    public async Task<IEnumerable<PromocionPaquete>> ObtenerTodoAsync()
    {
        // CORREGIDO: Se usa 'PromocionPaquetes' en plural
        return await _dbContext.PromocionPaquetes.Include(pP => pP.Promocion).ToListAsync();
    }
    public async Task<PromocionPaquete?> ObtenerPorIdAsync(int[] id)
    {
        if (id.Length != 2) throw new ArgumentException("La cadena recibida debe contener solo dos elementos");
        // CORREGIDO: Se usa 'PromocionPaquetes' en plural
        return await _dbContext.PromocionPaquetes.Include(pP => pP.Promocion).SingleOrDefaultAsync(pP => pP.Idpromocion == id[0] && pP.Idpaquete == id[1]);
    }
    public async Task<bool> EliminarAsync(PromocionPaquete record)
    {
        // CORREGIDO: Se usa 'PromocionPaquetes' en plural
        _dbContext.PromocionPaquetes.Remove(record);
        return await _dbContext.SaveChangesAsync() > 0;
    }
    public async Task<IEnumerable<PromocionPaquete>> ObtenerPorReferencia(int id, string nombreIdentificador)
    {
        // CORREGIDO: Se usa 'PromocionPaquetes' en plural
        return (await _dbContext.PromocionPaquetes.Include(pP => pP.Promocion).ToListAsync()).Where(pC => OperadorObj<PromocionPaquete, int>.Comparar(pC, nombreIdentificador, id));
    }
    public Task<bool> ActualizarAsync(PromocionPaquete record) => throw new NotImplementedException();
}