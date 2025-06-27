namespace megaapi.models;

public class Contrato()
{
  public int Idcontrato { set; get; }
  public int Idsuscriptor { set; get; }
  public DateTime FechaContr { set; get; }
  public DateTime? FechaFin { set; get; }
  public int PrecioBase { set; get; }

  // Relaciones.
  public Suscriptor Suscriptor { set; get; } = null!;
  public ICollection<ContratoPaquete> Paquetes { set; get; } = null!;
  public ICollection<PromocionContrato> Promociones { set; get; } = null!;
  public ICollection<PromoPersonalizada> PromosPersonalizadas { set; get; } = null!;
}