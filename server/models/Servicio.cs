namespace megaapi.models;

public class Servicio()
{
  public int Idservicio { set; get; }
  public int? Cantidad { set; get; }
  public float PrecioBase { set; get; }
  public int Tipo { set; get; }
  public ICollection<PaqueteServicio> Paquetes { set; get; } = null!;
}