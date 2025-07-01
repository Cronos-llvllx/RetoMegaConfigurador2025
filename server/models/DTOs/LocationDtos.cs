namespace megaapi.models.DTOs;

/// <summary>
/// DTO simplificado para Ciudad que coincide con las expectativas del frontend
/// </summary>
public class CiudadDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
}

/// <summary>
/// DTO simplificado para Colonia que coincide con las expectativas del frontend
/// </summary>
public class ColoniaDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public int CiudadId { get; set; }
    public string? CiudadName { get; set; }
}
