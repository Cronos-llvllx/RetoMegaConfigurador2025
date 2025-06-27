namespace megaapi.models;

public class Paquete()
{
  public int Idpaquete { set; get; }
  public string Nombre { set; get; } = null!;
  public float PrecioBase { set; get; }
  public int Tipo { set; get; }

  // Relaciones.
  public ICollection<PaqueteServicio> Servicios { set; get; } = null!;
  public ICollection<ContratoPaquete> Contratos { set; get; } = null!;
  public ICollection<PromocionPaquete> Promociones { set; get; } = null!;
}