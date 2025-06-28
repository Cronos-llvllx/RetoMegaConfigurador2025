namespace megaapi.models;

public class PromocionContrato()
{
  public int Idpromocion { set; get; }
  public int Idcontrato { set; get; }

  // Relaciones.
  public Promocion Promocion { set; get; } = null!;
  public Contrato Contrato { set; get; } = null!;
}