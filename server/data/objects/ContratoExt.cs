using megaapi.models;
using System.Collections.Generic;

namespace megaapi.data.objects;

/// <summary>
///  Definición para Contato. Las clases Ext son extensiones de las clases de las que heredan.
/// </summary>
public class ContratoExt : Contrato
{
    /// <summary>Lista de paquetes ligados al contrato.</summary>
    // CORREGIDO: Se añade 'new' para indicar que esta propiedad oculta intencionadamente a la de la clase base.
    // También se corrige el nombre de 'Paquete' a 'Paquetes' para ser consistente.
    public new ICollection<PaqueteContratoExt> Paquetes { get; set; } = new List<PaqueteContratoExt>();
}