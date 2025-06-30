namespace megaapi.models.DTOs;

/// <summary>
/// DTO for adding packages to contracts
/// </summary>
public class ContratoPaqueteDto
{
    public int Idcontrato { get; set; }
    public int Idpaquete { get; set; }
}

/// <summary>
/// DTO for contract package responses
/// </summary>
public class ContratoPaqueteResponseDto
{
    public int Idcontrato { get; set; }
    public int Idpaquete { get; set; }
    public DateTime FechaAdicion { get; set; }
    public DateTime? FechaRetiro { get; set; }
}
