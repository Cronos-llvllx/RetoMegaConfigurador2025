namespace megaapi.models;

public class Suscriptor()
{
  public int Idsuscriptor { set; get; }
  public int Idcolonia { set; get; }
  public string Email { set; get; } = null!;
  public string Nombre { set; get; } = null!;
  public string Telefono { set; get; } = null!;
  public byte Tipo { set; get; }
  public Colonia Colonia { set; get; } = null!;
  public Contrato Contrato { set; get; } = null!;
}