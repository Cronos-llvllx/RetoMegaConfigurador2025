namespace megaapi.models;
public class PromocionColonia
{
    public int Idpromocion { get; set; }
    public int Idcolonia { get; set; }
    // Relaciones
    public virtual Promocion Promocion { get; set; } = null!;
    public virtual Colonia Colonia { get; set; } = null!;
}