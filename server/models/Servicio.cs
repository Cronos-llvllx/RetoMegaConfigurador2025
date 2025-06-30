namespace megaapi.models;
public class Servicio
{
    public int Idservicio { get; set; }
    public int? Cantidad { get; set; }
    public decimal PrecioBase { get; set; }
    public byte Tipo { get; set; }
    // Relaciones
    public virtual ICollection<PaqueteServicio> Paquetes { get; set; } = new List<PaqueteServicio>();
}