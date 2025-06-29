using megaapi.models;

namespace megaapi.interfaces;

/// <summary>Interfaz para Suscriptor.</summary>
public interface ISuscriptor : IEntidad<Suscriptor, int>
{
  /// <summary>
  /// Obtiene a todos los suscriptores, incluyendo colonia y ciudad.
  /// </summary>
}