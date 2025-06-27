namespace megaapi.Models
{


    public class NeighborhoodPromotion
    {
        public int IdNeighborhoodPromotion { get; set; } // Clave primaria para la entidad "promoción de colonia"
        public int IdNeighborhood { get; set; } // Clave foránea para la entidad "colonia"
        public int IdCityPromotion { get; set; } // Clave foránea para la entidad "promoción de ciudad"
        public int IdPromotionalPackage { get; set; } // Clave foránea para la entidad "paquete promocional"
    }
}