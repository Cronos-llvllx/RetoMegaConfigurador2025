namespace megaapi.Models
{
    public class Package
    {
        public int IdPackage { get; set; } // Clave primaria para la entidad"paquete"
        public string Name { get; set; } // Nombre del paquete
        public string Type { get; set; } //Tipo de paquete (por ejemplo, "Paquete de servicios", "Paquete de productos", etc.)
        public decimal BasePrice { get; set; } // Precio base del paquete "Por ejemplo 600.00" (sin IVA)
        // Propiedades de navegacion para la relacion muchos a muchos
        public ICollection<ServicePackage> PackageServices { get; set; }
    }
}
