namespace megaapi.models;

public class Promocion()
{
  public int Idpromocion { set; get; }
  public byte Alcance { set; get; }
  public string Nombre { set; get; } = null!;
  public int? Duracion { set; get; }
  public DateTime FechaRegistro { set; get; }
  public decimal PrecioPorcen { set; get; }
  public byte Tipo { set; get; }
  public DateTime Vigencia { set; get; }
}