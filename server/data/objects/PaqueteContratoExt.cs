using megaapi.models;

namespace megaapi.data.objects;

/// <summary>
///  Definición para Contato. Las clases Obj son extensiones de las clases de las que heredan.
/// </summary>
public class PaqueteContratoExt : Paquete
{

  /// <summary>Fecha en la que el paquete fue agregado a un contrato.</summary>
  public DateTime FechaAdicion { get; set; }
  /// <summary>Fecha en la que el paquete fue quitado de un contrato.</summary>
  public DateTime? FechaRetiro { get; set; }
  /// <summary>Lista de servicios ligados al paquete.</summary>
  public ICollection<Servicio> Servicios { get; set; } = null!;
}