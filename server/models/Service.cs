namespace megaapi.Models
{


    public class Service
    {
        public int IdService { get; set; } // Clave primaria para la entidad "servicio"
        public int Type { get; set; } // Tipo de servicio (1.Telefonía, 2.TV, 3.Internet)
        public int Amount { get; set; } // la cantidad es una propiedad de servicio y no de paquete.Implica la cantidad que ofrece el servicio durante un periodo de tiempo (mensual). Por ejemplo:
                                        //Telefonía: no. de líneas.
                                        //Internet: megas totales.
                                        //TV: canales totales.
                                        //Si es NULL o menor o igual a 0, el servicio es ilimitado en cantidad.

        public decimal BasePrice { get; set; }// indica el precio base del servicio (sin IVA). Por ejemplo: 599.00
                                              // Propiedad de navegación para la relación muchos a muchos
        public ICollection<ServicePackage> PackageServices { get; set; } // O directamente ICollection<Package>
    }
}