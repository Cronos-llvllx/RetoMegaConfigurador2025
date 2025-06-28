namespace megaapi.models;

public class Paquete()
{
  public int Idpaquete { set; get; }
  public string Nombre { set; get; } = null!;
  public decimal PrecioBase { set; get; }
  public byte Tipo { set; get; }

  // Relaciones.
  public ICollection<PaqueteServicio> Servicios { set; get; } = null!;
  public ICollection<ContratoPaquete> Contratos { set; get; } = null!;
  public ICollection<PromocionPaquete> Promociones { set; get; } = null!;
}