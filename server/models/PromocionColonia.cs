namespace megaapi.models;

public class PromocionColonia()
{
  public int Idpromocion { set; get; }
  public int Idcolonia { set; get; }

  // Relaciones.
  public Promocion Promocion { set; get; } = null!;
  public Colonia Colonia { set; get; } = null!;
}