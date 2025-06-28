namespace megaapi.models;

public class Colonia()
{
  public int IdColonia { set; get; }
  public int Idciudad { set; get; }
  public string Nombre { set; get; } = null!;
  // Referencias.
  public Ciudad Ciudad { get; set; } = null!;
}
