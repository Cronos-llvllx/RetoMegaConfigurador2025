using megaapi.data;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace megaapi.repositories
{
    /// <summary>Repositorio de suscriptores.</summary>
    /// <param name="dbContext">Inyección de dependencia DbContext.</param>
    public class RepoSuscriptor(MEGADbContext dbContext) : ISuscriptor
    {
        private readonly MEGADbContext _dbContext = dbContext;

        public async Task<Suscriptor> CreateAsync(Suscriptor entity)
        {
            await _dbContext.Suscriptores.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Suscriptor>> GetAllAsync()
        {
            return await _dbContext.Suscriptores.ToListAsync();
        }

        public async Task<Suscriptor?> GetByIdAsync(int id)
        {
            return await _dbContext.Suscriptores.FindAsync(id);
        }

        public async Task<bool> RemoveAsync(Suscriptor entity)
        {
            _dbContext.Suscriptores.Remove(entity);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> UpdateAsync(Suscriptor entity)
        {
            _dbContext.Suscriptores.Update(entity);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}