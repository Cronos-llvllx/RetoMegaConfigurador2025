using megaapi.data;
using megaapi.interfaces;
using megaapi.models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;

namespace megaapi.repositories
{
    /// <summary>Repositorio de contratos.</summary>
    /// <param name="dbContext">Inyección de dependencia DbContext.</param>
    public class RepoContrato(MEGADbContext dbContext) : IContrato
    {
        private readonly MEGADbContext _dbContext = dbContext;

        public async Task<Contrato> CreateAsync(Contrato entity)
        {
            await _dbContext.Contratos.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<IEnumerable<Contrato>> GetAllAsync()
        {
            return await _dbContext.Contratos.ToListAsync();
        }

public async Task<Contrato?> GetByIdAsync(int id)
        {
            // Esta consulta es crucial para tu lógica de cálculo de deuda.
            // Carga el contrato y todas sus relaciones anidadas (Suscriptor, Paquetes, Servicios).
            return await _dbContext.Contratos
                .Include(c => c.Suscriptor)
                .Include(c => c.Paquetes!) // Incluye la colección de paquetes del contrato
                    .ThenInclude(cp => cp.Paquete) // Incluye los paquetes de la relación
                        .ThenInclude(p => p.Servicios!)
                            .ThenInclude(ps => ps.Servicio)
                .FirstOrDefaultAsync(c => c.Idcontrato == id);
        }
        
        public async Task<bool> UpdateAsync(Contrato entity)
        {
            _dbContext.Contratos.Update(entity);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> RemoveAsync(Contrato entity)
        {
            _dbContext.Contratos.Remove(entity);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }
    }
}