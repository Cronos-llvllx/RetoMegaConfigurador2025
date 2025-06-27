namespace megaapi.models;

public class Colonia()
{
  public int IdColonia { set; get; }
  public int Idciudad { set; get; }
  public string Nombre { set; get; } = null!;
  public Ciudad Ciudad { set; get; } = null!;

  // Relación.
  public ICollection<Suscriptor> Suscriptores { set; get; } = null!;
  public ICollection<PromocionColonia> Promociones { set; get; } = null!;
}
