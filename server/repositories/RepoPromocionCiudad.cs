using megaapi.data;
using megaapi.data.objects;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace megaapi.repositories;
public class RepoPromocionCiudad(MEGADbContext dbContext) : IPromocionCiudad
{
    private readonly MEGADbContext _dbContext = dbContext;
    public async Task<PromocionCiudad> CrearAsync(PromocionCiudad record)
    {
        // CORREGIDO: Se usa 'PromocionCiudades' en plural
        await _dbContext.PromocionCiudades.AddAsync(record);
        await _dbContext.SaveChangesAsync();
        return record;
    }
    public async Task<IEnumerable<PromocionCiudad>> ObtenerTodoAsync()
    {
        // CORREGIDO: Se usa 'PromocionCiudades' en plural
        return await _dbContext.PromocionCiudades.Include(pC => pC.Promocion).ToListAsync();
    }
    public async Task<PromocionCiudad?> ObtenerPorIdAsync(int[] id)
    {
        if (id.Length != 2) throw new ArgumentException("La cadena recibida debe contener solo dos elementos");
        // CORREGIDO: Se usa 'PromocionCiudades' en plural
        return await _dbContext.PromocionCiudades.Include(pC => pC.Promocion).SingleOrDefaultAsync(pC => pC.Idpromocion == id[0] && pC.Idciudad == id[1] );
    }
    public async Task<bool> EliminarAsync(PromocionCiudad record)
    {
        // CORREGIDO: Se usa 'PromocionCiudades' en plural
        _dbContext.PromocionCiudades.Remove(record);
        return await _dbContext.SaveChangesAsync() > 0;
    }
    public async Task<IEnumerable<PromocionCiudad>> ObtenerPorReferencia(int id, string nombreIdentificador)
    {
        // CORREGIDO: Se usa 'PromocionCiudades' en plural
        return (await _dbContext.PromocionCiudades.Include(pC => pC.Promocion).ToListAsync()).Where(pC => OperadorObj<PromocionCiudad, int>.Comparar(pC, nombreIdentificador, id));
    }
    public Task<bool> ActualizarAsync(PromocionCiudad record) => throw new NotImplementedException();
}