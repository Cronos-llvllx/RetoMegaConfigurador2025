using megaapi.data;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace megaapi.repositories;
public class RepoPromocion(MEGADbContext dbContext) : IPromocion
{
    private readonly MEGADbContext _dbContext = dbContext;
    public async Task<IEnumerable<Promocion>> ObtenerTodoAsync()
    {
        // CORREGIDO: Se usa 'Promociones' en plural
        return await _dbContext.Promociones.ToListAsync();
    }
    public async Task<Promocion?> ObtenerPorIdAsync(int id)
    {
        // CORREGIDO: Se usa 'Promociones' en plural
        return await _dbContext.Promociones.FindAsync(id);
    }
    public Task<Promocion> CrearAsync(Promocion promocion) => throw new NotImplementedException();
    public Task<bool> ActualizarAsync(Promocion promocion) => throw new NotImplementedException();
    public Task<bool> EliminarAsync(Promocion promocion) => throw new NotImplementedException();
}