namespace megaapi.models;

public class Paquete()
{
  public int Idpaquete { set; get; }
  public string Nombre { set; get; } = null!;
  public decimal PrecioBase { set; get; }
  public byte Tipo { set; get; }
}