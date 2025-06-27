export interface Promotion {
        IdPromotion: number; // Clave primaria para la entidad "promoción"
        Scope: number;  // Alcance de la promoción (1: Nuevos suscriptores, 2: todos los suscriptores)
        Duration: number;  //  a partir de la aplicación. No aplica para el precio de contratación.
        Name: string;  // Nombre de la promoción
        PriceOrPorcentage: number;   // Precio o porcentaje de la promoción. Cuando es un número entre cero y uno, se considera como un porcentaje (multiplicado por el precio o conjunto de precios para obtener el descuento). Si no, se considera como el precio base (reemplaza al precio o precios en conjunto).
        Type: number;  // Tipo de promoción (1: Promocion en precio de contratacion, 2: Promocion para servicios(pago mensual))
        Validity: Date;  // Fecha que la promoción ya no estara disponible al publico
}
