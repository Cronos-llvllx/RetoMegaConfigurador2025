namespace megaapi.models;

public class PaqueteServicio()
{
  public int Idpaquete { set; get; }
  public int Idservicio { set; get; }

  // Relaciones.
  public Paquete Paquete { set; get; } = null!;
  public Servicio Servicio { set; get; } = null!;
}