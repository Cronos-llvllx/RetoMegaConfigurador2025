namespace megaapi.Models
{
    public class Promotion
    {
        public int IdPromotion { get; set; } // Clave primaria para la entidad "promoción"  
        public int Scope { get; set; } // Alcance de la promoción (1: Nuevos suscriptores, 2: todos los suscriptores)
        public int Duration { get; set; } //  a partir de la aplicación. No aplica para el precio de contratación.
        public string Name { get; set; } // Nombre de la promoción
        public decimal PriceOrPorcentage { get; set; } // Precio o porcentaje de la promoción. Cuando es un número entre cero y uno, se considera como un porcentaje (multiplicado por el precio o conjunto de precios para obtener el descuento). Si no, se considera como el precio base (reemplaza al precio o precios en conjunto).
        public int Type { get; set; } // Tipo de promoción (1: Promocion en precio de contratacion, 2: Promocion para servicios(pago mensual))
        public DateTime Validity { get; set; } // Fecha que la promoción ya no estara disponible al publico
    }
}
