namespace megaapi.Models
{
    public class ContractPackage
    {
        public int IdContract { get; set; } // Clave foránea para la entidad "contrato"
        public Contract Contract { get; set; } = null!; // Propiedad de navegación para la relación con el contrato
        public int IdPackage { get; set; } // Clave foránea para la entidad "paquete"
        public Package Package { get; set; } = null!; // Propiedad de navegación para la relación con el paquete
        public DateTime AddedTime { get; set; } // Fecha y hora en que se agregó el paquete al contrato
        public DateTime? RemovedTime { get; set; } // Fecha y hora en que se eliminó el paquete del contrato (si aplica)
    }

}