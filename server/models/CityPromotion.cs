namespace megaapi.Models
{
    public class CityPromotion
    {
        public int IdCityPromotion { get; set; } // Clave primaria para la entidad "promoción de ciudad"
        public int IdCity { get; set; } // Clave foránea para la entidad "ciudad"
    }
}