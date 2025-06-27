using megaapi.Models; // Asegúrate de que el namespace sea correcto
namespace megaapi.Interfaces
{
    public interface IPromotionService
    {
        // Método para aplicar promociones al cálculo de la deuda.
        // Obed necesitará este método para su lógica de cálculo.
        decimal ApplyPromotions(decimal basePrice, IEnumerable<Promotion> promotions, Subscriber suscriptor);

        // Método para obtener las promociones aplicables a un suscriptor.
        IEnumerable<Promotion> GetApplicablePromotions(Subscriber suscriptor);
        // Método para obtener promociones por tipo (ej. solo de contratación)
        IEnumerable<Promotion> GetPromotionsByType(int type);

        // Método para obtener una promoción por su ID.
        Promotion? GetPromotionById(int id);

        // Método para crear una promoción (Marlene)
        void CreatePromotion(Promotion promotion);

        // Método para actualizar una promoción existente (Marlene)
        void UpdatePromotion(Promotion promotion);

        // Método para consultar promociones activas (Marlene lo usará en su gestor)
        IEnumerable<Promotion> GetActivePromotions();

        // Método para guardar los cambios
        void SaveChanges();
    }
} 