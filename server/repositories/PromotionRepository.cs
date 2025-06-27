using megaapi.Interfaces;
using megaapi.Models; // Para usar la clase Promotion
using System;
using System.Collections.Generic;
using System.Linq;

namespace megaapi.Repositories
{
    // Esta clase implementa la interfaz IPromotionService, por lo que debe definir todos sus métodos.
    public class PromotionRepository : IPromotionService
    {
        // ----------------------------------------------------------------------
        // SIMULACIÓN DE LA BASE DE DATOS EN MEMORIA para las promociones
        // ----------------------------------------------------------------------
        private static readonly List<Promotion> _promotions = new List<Promotion>();
        private static int _nextId = 1; // Para simular el auto-incremento de Idpromocion

        public PromotionRepository()
        {
            // Opcional: inicializa algunos datos de prueba si la lista está vacía
            if (!_promotions.Any())
            {
                // Promoción para precio de contratación (Tipo = 1)
                _promotions.Add(new Promotion
                {
                    IdPromotion = _nextId++, // <<-- CORREGIDO
                    Name = "Día del padre",  // <<-- CORREGIDO
                    Scope = 1,               // <<-- CORREGIDO
                    Duration = 0,            // <<-- CORREGIDO
                    PriceOrPorcentage = 0.50m, // <<-- CORREGIDO
                    Type = 1,                // <<-- CORREGIDO
                    Validity = new DateTime(2025, 6, 30) // <<-- CORREGIDO
                });

                // Promoción para pago mensual (Tipo = 2)
                _promotions.Add(new Promotion
                {
                    IdPromotion = _nextId++, // <<-- CORREGIDO
                    Name = "6 meses de Internet Gratis", // <<-- CORREGIDO
                    Scope = 2, // Para todos los suscriptores
                    Duration = 6, // 6 meses de duración [cite: 25]
                    PriceOrPorcentage = 0.00m, // 100% de descuento
                    Type = 2, // Promoción para servicios (pago mensual) [cite: 25]
                    Validity = new DateTime(2025, 12, 31) // Vigente hasta fin de año
                });
            }
        }

        // ----------------------------------------------------------------------
        // IMPLEMENTACIÓN DE MÉTODOS DE LÓGICA DE NEGOCIO (simulados)
        // Nota: Estos métodos deberían estar en un 'PromotionService', no en un 'Repository'.
        // Pero los implementamos aquí para cumplir con la interfaz.
        // ----------------------------------------------------------------------

        // Este método NO es una tarea de repositorio, es lógica de negocio.
        // Aquí solo lanzamos una excepción para indicar que no está implementado.
        public decimal ApplyPromotions(decimal basePrice, IEnumerable<Promotion> promotions, Subscriber suscriptor)
        {
            // Esta lógica es compleja y es responsabilidad de Obed.
            // No la implementamos aquí, solo cumplimos con la firma del método.
            throw new NotImplementedException("La lógica de aplicación de promociones no se implementa en el repositorio. Es responsabilidad del servicio de cálculo de deuda.");
        }

        // Este método NO es una tarea de repositorio, es lógica de negocio.
        // La implementación real requiere acceder a las relaciones (Promocion-Paquete, etc.).
        public IEnumerable<Promotion> GetApplicablePromotions(Subscriber suscriptor)
        {
            // Esta lógica es compleja. Simulamos un resultado simple.
            // La implementación real es responsabilidad de Obed y Marlene.
            throw new NotImplementedException("La lógica para obtener promociones aplicables no está implementada en el repositorio. Es una regla de negocio compleja.");
        }

        // ----------------------------------------------------------------------
        // IMPLEMENTACIÓN DE MÉTODOS DE ACCESO A DATOS (Repositorio puro)
        // ----------------------------------------------------------------------

        public Promotion? GetPromotionById(int id)
{
    // Usa LINQ para buscar en la lista en memoria.
    // FirstOrDefault() devuelve null si no encuentra el elemento.
    return _promotions.FirstOrDefault(p => p.IdPromotion == id);
}
        public void CreatePromotion(Promotion promotion)
        {
            // <<-- REVISAR NOMBRE DE LA PROPIEDAD EN TU MODELO.
            // Si la propiedad se llama 'IdPromotion', este código es correcto
            if (promotion.IdPromotion == 0)
            {
                promotion.IdPromotion = _nextId++;
            }
            _promotions.Add(promotion);
        }

        public void UpdatePromotion(Promotion promotion)
        {
            var existingPromotion = _promotions.FirstOrDefault(p => p.IdPromotion == promotion.IdPromotion); // <<-- CORREGIDO
            if (existingPromotion != null)
            {
                // Actualiza las propiedades con los nuevos valores
                existingPromotion.Name = promotion.Name; // <<-- CORREGIDO
                existingPromotion.Scope = promotion.Scope; // <<-- CORREGIDO
                existingPromotion.Duration = promotion.Duration; // <<-- CORREGIDO
                existingPromotion.PriceOrPorcentage = promotion.PriceOrPorcentage; // <<-- CORREGIDO
                existingPromotion.Type = promotion.Type; // <<-- CORREGIDO
                existingPromotion.Validity = promotion.Validity; // <<-- CORREGIDO
            }
        }

        public IEnumerable<Promotion> GetActivePromotions()
        {
            // Filtra promociones por la fecha de vigencia.
            // Según el diccionario de datos, Vigencia indica la fecha en que ya no está disponible al público[cite: 10].
            return _promotions.Where(p => p.Validity >= DateTime.Today); // <<-- CORREGIDO
        }

        public IEnumerable<Promotion> GetPromotionsByType(int type)
        {
            // Filtra promociones por el tipo (ej. 1 para contratación, 2 para servicio mensual)[cite: 10].
            return _promotions.Where(p => p.Type == type); // <<-- CORREGIDO
        }

        public void SaveChanges()
        {
            // En esta versión en memoria, no se necesita hacer nada.
        }
    }
}