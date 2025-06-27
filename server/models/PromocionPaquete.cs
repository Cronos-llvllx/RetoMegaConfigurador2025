namespace megaapi.models;

public class PromocionPaquete()
{
  public int Idpromocion { set; get; }
  public int Idpaquete { set; get; }

  // Relaciones.
  public Promocion Promocion { set; get; } = null!;
  public Paquete Paquete { set; get; } = null!;
}