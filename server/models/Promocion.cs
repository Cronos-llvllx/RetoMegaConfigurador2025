namespace megaapi.models;

public class Promocion()
{
  public int Idpromocion { set; get; }
  public byte Alcance { set; get; }
  public string Nombre { set; get; } = null!;
  public int? Duracion { set; get; }
  public DateTime FechaRegistro { set; get; }
  public decimal PrecioBase { set; get; }
  public byte Tipo { set; get; }
  public DateTime Vigencia { set; get; }

  // Relaciones.
  public ICollection<PromocionCiudad> Ciudades { set; get; } = null!;
  public ICollection<PromocionColonia> Colonias { set; get; } = null!;
  public ICollection<PromocionPaquete> Paquetes { set; get; } = null!;
  public ICollection<PromocionContrato> Contratos { set; get; } = null!;
}