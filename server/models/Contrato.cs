using System;
using System.Collections.Generic; // Asegúrate de tener esta directiva para ICollection

namespace megaapi.models
{
    // Clase que representa la entidad 'Contrato' en la base de datos.
    // Utiliza el constructor primario de C#
    public class Contrato
    {
        // Constructor para inicializar las colecciones de relaciones.
        // Esto evita errores de referencia nula al acceder a ellas.
        public Contrato()
        {
            Paquetes = new HashSet<ContratoPaquete>();
            Promociones = new HashSet<PromocionContrato>();
            PromosPersonalizadas = new HashSet<PromoPersonalizada>();
        }

        // Propiedades que mapean a las columnas de la tabla.
        public int Idcontrato { get; set; }
        public int Idsuscriptor { get; set; }
        public DateTime FechaContr { get; set; }
        public DateTime? FechaFin { get; set; } 
        public decimal PrecioBase { get; set; } 

        // Propiedades de navegación para las relaciones con otras entidades.
        public Suscriptor Suscriptor { get; set; } = null!;
        public ICollection<ContratoPaquete> Paquetes { get; set; } // Relación n:m con Paquete (a través de ContratoPaquete)
        public ICollection<PromocionContrato> Promociones { get; set; } // Relación n:m con Promocion
        public ICollection<PromoPersonalizada> PromosPersonalizadas { get; set; } // Relación 1:n con PromoPersonalizada
    }
}