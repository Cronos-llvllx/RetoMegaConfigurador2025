namespace megaapi.models;

public class ContratoPaquete()
{
  public int Idcontrato { set; get; }
  public int Idpaquete { set; get; }
  public DateTime FechaAdicion { set; get; }
  public DateTime? FechaRetiro { set; get; }

  // Relaciones
  public Contrato Contrato { set; get; } = null!;
  public Paquete Paquete { set; get; } = null!;
}