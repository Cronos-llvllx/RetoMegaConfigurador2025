namespace megaapi.models;
public class ContratoPaquete
{
    public int Idcontrato { get; set; }
    public int Idpaquete { get; set; }
    public DateTime FechaAdicion { get; set; }
    public DateTime? FechaRetiro { get; set; }
    // Relaciones
    public virtual Contrato Contrato { get; set; } = null!;
    public virtual Paquete Paquete { get; set; } = null!;
}