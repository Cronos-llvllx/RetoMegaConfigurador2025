namespace megaapi.models;

public class Servicio()
{
  public int Idservicio { set; get; }
  public int? Cantidad { set; get; }
  public decimal PrecioBase { set; get; }
  public byte Tipo { set; get; }
  public ICollection<PaqueteServicio> Paquetes { set; get; } = null!;
}