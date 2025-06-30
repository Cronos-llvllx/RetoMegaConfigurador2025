namespace megaapi.models;
public class PromocionContrato
{
    public int Idpromocion { get; set; }
    public int Idcontrato { get; set; }
    // Relaciones
    public virtual Promocion Promocion { get; set; } = null!;
    public virtual Contrato Contrato { get; set; } = null!;
}