namespace megaapi.models;
public class Ciudad
{
    public int Idciudad { get; set; }
    public string Nombre { get; set; } = null!;
    // Relaciones: Una ciudad tiene muchas colonias y muchas promociones
    public virtual ICollection<Colonia> Colonias { get; set; } = new List<Colonia>();
    public virtual ICollection<PromocionCiudad> Promociones { get; set; } = new List<PromocionCiudad>();
}