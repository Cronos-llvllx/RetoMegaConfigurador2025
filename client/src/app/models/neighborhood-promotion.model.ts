export interface NeighborhoodPromotion {
        IdNeighborhoodPromotion: number;  // Clave primaria para la entidad "promoción de colonia"
        IdNeighborhood: number;  // Clave foránea para la entidad "colonia"
        IdCityPromotion: number;  // Clave foránea para la entidad "promoción de ciudad"
        IdPromotionalPackage: number; // Clave foránea para la entidad "paquete promocional"
}
