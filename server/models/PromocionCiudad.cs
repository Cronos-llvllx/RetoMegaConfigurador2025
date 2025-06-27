namespace megaapi.models;

public class PromocionCiudad()
{
  public int Idpromocion { set; get; }
  public int Idciudad { set; get; }

  // Relaciones.
  public Promocion Promocion { set; get; } = null!;
  public Ciudad Ciudad { set; get; } = null!;
}