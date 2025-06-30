namespace megaapi.models;
public class PromoPersonalizada
{
    public int Idpromopersonalizada { get; set; }
    public int Idcontrato { get; set; }
    public DateTime FechaAplicacion { get; set; }
    public string Descripcion { get; set; } = null!;
    public decimal PrecioPorcen { get; set; }
    // Relaciones
    public virtual Contrato Contrato { get; set; } = null!;
}