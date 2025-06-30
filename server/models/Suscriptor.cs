namespace megaapi.models;
public class Suscriptor
{
    public int Idsuscriptor { get; set; }
    public int Idcolonia { get; set; }
    public string Email { get; set; } = null!;
    public string Nombre { get; set; } = null!;
    public string Telefono { get; set; } = null!;
    public byte Tipo { get; set; }
    // Relaciones
    public virtual Colonia Colonia { get; set; } = null!;
    public virtual Contrato Contrato { get; set; } = null!;
}
