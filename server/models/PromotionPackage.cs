namespace megaapi.Models
{


    public class PromotionalPackage
    {
        public int IdPromotionalPackage { get; set; } // Clave primaria para la entidad "paquete promocional"
        public int IdPackage { get; set; } // Clave foránea para la entidad "paquete"

    }
}