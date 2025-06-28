using megaapi.data;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;

namespace megaapi.repositories
{
    /// <summary>Repositorio de promociones.</summary>
    /// <param name="dbContext">Inyección de dependencia DbContext.</param>
    public class RepoPromocion(MEGADbContext dbContext) : IPromocion
    {
        private readonly MEGADbContext _dbContext = dbContext;

        public async Task<Promocion> CreateAsync(Promocion entity)
        {
            await _dbContext.Promociones.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Promocion>> GetAllAsync()
        {
            return await _dbContext.Promociones.ToListAsync();
        }

        public async Task<Promocion?> GetByIdAsync(int id)
        {
            return await _dbContext.Promociones.FindAsync(id);
        }

        public async Task<bool> RemoveAsync(Promocion entity)
        {
            _dbContext.Promociones.Remove(entity);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateAsync(Promocion entity)
        {
            _dbContext.Promociones.Update(entity);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}