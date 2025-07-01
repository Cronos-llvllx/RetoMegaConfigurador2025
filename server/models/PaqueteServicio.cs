namespace megaapi.models;
public class PaqueteServicio
{
    public int Idpaquete { get; set; }
    public int Idservicio { get; set; }
    // Relaciones
    public virtual Paquete Paquete { get; set; } = null!;
    public virtual Servicio Servicio { get; set; } = null!;
}