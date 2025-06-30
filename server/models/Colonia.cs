namespace megaapi.models;
public class Colonia
{
    public int Idcolonia { get; set; }
    public int Idciudad { get; set; }
    public string Nombre { get; set; } = null!;
    // Relaciones: Una colonia pertenece a una ciudad y tiene muchos suscriptores y promociones
    public virtual Ciudad Ciudad { get; set; } = null!;
    public virtual ICollection<Suscriptor> Suscriptores { get; set; } = new List<Suscriptor>();
    public virtual ICollection<PromocionColonia> Promociones { get; set; } = new List<PromocionColonia>();
}