using megaapi.data;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace megaapi.repositories;

public class RepoPaquete(MEGADbContext dbContext) : IPaquete
{
    private readonly MEGADbContext _dbContext = dbContext;
    public async Task<IEnumerable<Paquete>> ObtenerTodoAsync()
    {
        // CORREGIDO: Se usa 'Paquetes' en plural
        return await _dbContext.Paquetes.ToListAsync();
    }
}