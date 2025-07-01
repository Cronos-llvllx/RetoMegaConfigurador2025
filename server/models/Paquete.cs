namespace megaapi.models;
public class Paquete
{
    public int Idpaquete { get; set; }
    public string Nombre { get; set; } = null!;
    public decimal PrecioBase { get; set; }
    public byte Tipo { get; set; }
    // Relaciones
    public virtual ICollection<ContratoPaquete> Contratos { get; set; } = new List<ContratoPaquete>();
    public virtual ICollection<PaqueteServicio> Servicios { get; set; } = new List<PaqueteServicio>();
    public virtual ICollection<PromocionPaquete> Promociones { get; set; } = new List<PromocionPaquete>();
}