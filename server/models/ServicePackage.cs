namespace megaapi.Models {
    // Clase de unión (Join Entity) para la relación muchos a muchos (si es necesario un atributo adicional como Cantidad por paquete)
    public class ServicePackage
    {
        public int IdPackage { get; set; }// Clave foranea para la entidad "paquete-servicio"
        public Package Package { get; set; }//
        public int IdService { get; set; }// Clave foranea para la entidad "paquete-servicio"
        public Service Service { get; set; }
        // Si la "Cantidad" del Servicio es específica para cada paquete, iría aquí.
        // Si la cantidad es una característica inherente al Servicio mismo, se queda en Servicio.cs
        // Basado en ER, Cantidad está en Servicio, lo que implica que es una propiedad del servicio, no de la relación.
    }
}