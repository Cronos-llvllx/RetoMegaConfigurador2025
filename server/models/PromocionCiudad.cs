namespace megaapi.models;
public class PromocionCiudad
{
    public int Idpromocion { get; set; }
    public int Idciudad { get; set; }
    // Relaciones
    public virtual Promocion Promocion { get; set; } = null!;
    public virtual Ciudad Ciudad { get; set; } = null!;
}