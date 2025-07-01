namespace megaapi.models;
public class Contrato
{
    public int Idcontrato { get; set; }
    public int Idsuscriptor { get; set; }
    public DateTime FechaContr { get; set; }
    public DateTime? FechaFin { get; set; }
    public decimal PrecioBase { get; set; }
    // Relaciones
    public virtual Suscriptor Suscriptor { get; set; } = null!;
    public virtual ICollection<PromoPersonalizada> PromosPersonalizadas { get; set; } = new List<PromoPersonalizada>();
    public virtual ICollection<ContratoPaquete> Paquetes { get; set; } = new List<ContratoPaquete>();
    public virtual ICollection<PromocionContrato> Promociones { get; set; } = new List<PromocionContrato>();
}