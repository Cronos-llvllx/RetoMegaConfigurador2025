using megaapi.models;

namespace megaapi.data.objects;

/// <summary>
///  Definición para Contato. Las clases Ext son extensiones de las clases de las que heredan.
/// </summary>
public class ContratoExt : Contrato
{
  /// <summary>Lista de paquetes ligados al contrato.</summary>
  public ICollection<PaqueteContratoExt> Paquetes { get; set; } = null!;
}