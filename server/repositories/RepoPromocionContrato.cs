using megaapi.data;
using megaapi.data.objects;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace megaapi.repositories;
public class RepoPromocionContrato(MEGADbContext dbContext) : IPromocionContrato
{
    private readonly MEGADbContext _dbContext = dbContext;
    public async Task<PromocionContrato> CrearAsync(PromocionContrato record)
    {
        // CORREGIDO: Se usa 'PromocionContratos' en plural
        await _dbContext.PromocionContratos.AddAsync(record);
        await _dbContext.SaveChangesAsync();
        return record;
    }
    public async Task<IEnumerable<PromocionContrato>> ObtenerTodoAsync()
    {
        // CORREGIDO: Se usa 'PromocionContratos' en plural
        return await _dbContext.PromocionContratos.Include(pC => pC.Promocion).ToListAsync();
    }
    public async Task<PromocionContrato?> ObtenerPorIdAsync(int[] id)
    {
        if (id.Length != 2) throw new ArgumentException("La cadena recibida debe contener solo dos elementos");
        // CORREGIDO: Se usa 'PromocionContratos' en plural
        return await _dbContext.PromocionContratos.Include(pC => pC.Promocion).SingleOrDefaultAsync(pC => pC.Idpromocion == id[0] && pC.Idcontrato == id[1]);
    }
    public async Task<bool> EliminarAsync(PromocionContrato record)
    {
        // CORREGIDO: Se usa 'PromocionContratos' en plural
        _dbContext.PromocionContratos.Remove(record);
        return await _dbContext.SaveChangesAsync() > 0;
    }
    public async Task<IEnumerable<PromocionContrato>> ObtenerPorReferencia(int id, string nombreIdentificador)
    {
        // CORREGIDO: Se usa 'PromocionContratos' en plural
        return (await _dbContext.PromocionContratos.Include(pC => pC.Promocion).ToListAsync()).Where(pC => OperadorObj<PromocionContrato, int>.Comparar(pC, nombreIdentificador, id));
    }
    public Task<bool> ActualizarAsync(PromocionContrato record) => throw new NotImplementedException();
}