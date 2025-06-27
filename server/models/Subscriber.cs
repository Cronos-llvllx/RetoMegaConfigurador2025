namespace megaapi.Models
{
    public class Subscriber
    {
        public int IdSubscriber { get; set; } // Clave primaria para la entidad "suscriptor"
        public int IdNeighborhood { get; set; } // Clave foránea para la entidad "colonia"
        public string Email { get; set; } // Correo electrónico del suscriptor
        public string Name { get; set; } // Nombre del suscriptor
        public string PhoneNumber { get; set; } // Número de teléfono del suscriptor
        public int Type { get; set; } // Tipo de suscriptor (1.residencial, 2.empresarial) .Ayuda a clasificar los paquetes y promociones que a este le aplican.
        public ICollection<Contract>? Contracts { get; set; }

    }
}