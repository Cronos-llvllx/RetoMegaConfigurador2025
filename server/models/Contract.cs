namespace megaapi.Models
{
    public class Contract
    {
        public int IdContract { get; set; } // Clave primaria para la entidad "contrato"
        public int IdSubscriber { get; set; } // Clave foránea que relaciona el contrato con un suscriptor
        public DateTime StartTime { get; set; } // Fecha y hora del contrato inicio
        public DateTime EndTime { get; set; } // Fecha y hora del contrato fin
        public decimal PriceContract { get; set; } // Precio del contrato (sin IVA)
        public Subscriber? Subscriber { get; set; } // Propiedad de navegación

        // Agrega una propiedad de navegación para la relación con ContratoPaquete --->>
        public ICollection<ContractPackage> ContractPackages { get; set; } // Relación con la tabla de unión
    }
}
