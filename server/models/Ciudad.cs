namespace megaapi.models;

public class Ciudad()
{
  public int Idciudad { set; get; }
  public string Nombre { set; get; } = null!;

  // Relación.
  public ICollection<Colonia> Colonias { set; get; } = null!;
}