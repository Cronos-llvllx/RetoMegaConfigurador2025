namespace megaapi.models;
public class PromocionPaquete
{
    public int Idpromocion { get; set; }
    public int Idpaquete { get; set; }
    // Relaciones
    public virtual Promocion Promocion { get; set; } = null!;
    public virtual Paquete Paquete { get; set; } = null!;
}