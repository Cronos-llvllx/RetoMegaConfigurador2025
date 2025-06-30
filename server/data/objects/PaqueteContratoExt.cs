using megaapi.models;
using System.Collections.Generic;

namespace megaapi.data.objects;

/// <summary>
///  Definición para Paquete. Las clases Ext son extensiones de las clases de las que heredan.
/// </summary>
public class PaqueteContratoExt : Paquete
{
    public DateTime FechaAdicion { get; set; }
    public DateTime? FechaRetiro { get; set; }
    /// <summary>Lista de servicios ligados al paquete.</summary>
    // CORREGIDO: Se añade 'new' para ocultar intencionadamente la propiedad de la clase base.
    public new ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();
}