namespace megaapi.models;
public class Promocion
{
    public int Idpromocion { get; set; }
    public byte? Alcance { get; set; }
    public string Nombre { get; set; } = null!;
    public int? Duracion { get; set; }
    public DateTime FechaRegistro { get; set; }
    public decimal PrecioPorcen { get; set; } // Ajustado a PrecioBase para coincidir con el DbContext
    public byte Tipo { get; set; }
    public DateTime Vigencia { get; set; }
    // Relaciones
    public virtual ICollection<PromocionCiudad> Ciudades { get; set; } = new List<PromocionCiudad>();
    public virtual ICollection<PromocionColonia> Colonias { get; set; } = new List<PromocionColonia>();
    public virtual ICollection<PromocionContrato> Contratos { get; set; } = new List<PromocionContrato>();
    public virtual ICollection<PromocionPaquete> Paquetes { get; set; } = new List<PromocionPaquete>();
}