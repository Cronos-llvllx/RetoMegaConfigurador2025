namespace megaapi.models;

public class Suscriptor()
{
  public int Idsuscriptor { set; get; }
  public int Idcolonia { set; get; }
  public string Email { set; get; } = null!;
  public string Name { set; get; } = null!;
  public string Telefono { set; get; } = null!;
  public int Tipo { set; get; }
  public Colonia Colonia { set; get; } = null!;
  public Contrato Contrato { set; get; } = null!;
}