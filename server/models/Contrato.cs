namespace megaapi.models;

public class Contrato()
{
  public int Idcontrato { set; get; }
  public int Idsuscriptor { set; get; }
  public DateTime FechaContr { set; get; }
  public DateTime? FechaFin { set; get; }
  public decimal PrecioBase { set; get; }
}