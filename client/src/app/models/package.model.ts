export interface Package {
        IdPackage: number; // Clave primaria para la entidad "paquete"
        Name: string; // Nombre del paquete
        Type: string; // Tipo de paquete (por ejemplo, "Paquete de servicios", "Paquete de productos", etc.)
        BasePrice: number; // Precio base del paquete "Por ejemplo 600.00" (sin IVA)
}
