using megaapi.models;
using megaapi.interfaces;

namespace megaapi.interfaces;

/// <summary>Interfaz para Suscriptor.</summary>
public interface ISuscriptor : IEntidad<Suscriptor, int>
{
  
  /// <summary>
  /// Otiene a todos los suscriptores, incluyendo colonia y ciudad.
  /// </summary>
  Task<List<Suscriptor>> GetAll();
}